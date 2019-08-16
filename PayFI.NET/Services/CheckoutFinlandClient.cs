using Baseline;
using PayFI.NET.Library.Model.CheckoutFinland;
using PayFI.NET.Library.Model.CheckoutFinland.Payment;
using PayFI.NET.Model.Merchant;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayFI.NET
{
    //<summary>
    // References : https://koumoul.com/openapi-viewer/?url=https://checkoutfinland.github.io/psp-api/checkout-psp-api.yaml&proxy=false
    //</summary>
    public class CheckoutRequestHeaders
    {
        public static readonly string Account = "checkout-account";
        public static readonly string Algorithm = "checkout-algorithm";
        public static readonly string Method = "checkout-method";
        public static readonly string NOnce = "checkout-nonce";
        public static readonly string Timestamp = "checkout-timestamp";
        public static readonly string TransactionId = "checkout-transaction-id";

        public static readonly string Signature = "signature";
    }

    public class CheckoutFinlandClient
    {
        const string ApiUrl = "https://api.checkout.fi";
        private readonly IRestClient _client;

        // Header that should be client.DefaultHeaders
        private readonly IReadOnlyList<string> persistentHeader = new List<string>()
        {
            CheckoutRequestHeaders.Account, CheckoutRequestHeaders.Algorithm
        };

        // Header that changes for each request
        private readonly IReadOnlyList<string> scopedHeader = new List<string>()
        {
            CheckoutRequestHeaders.Method, CheckoutRequestHeaders.NOnce, CheckoutRequestHeaders.Timestamp
        };
        // TODO: Some request need checkout-transaction-id

        private readonly string _secretKey;
        private readonly string _accountId;

        private readonly IDictionary<string, string> defaultHeaders;

        public CheckoutFinlandClient(string accountId, string secretKey)
        {
            _client = new RestClient(ApiUrl)
                .UseSerializer(() => new JsonNetSerializer())
                .AddDefaultHeader(CheckoutRequestHeaders.Account, accountId)
                .AddDefaultHeader(CheckoutRequestHeaders.Algorithm, "sha256");

            defaultHeaders = new Dictionary<string, string>()
            {
                { CheckoutRequestHeaders.Account, accountId },
                { CheckoutRequestHeaders.Algorithm, "sha256" }
            };

            _accountId = accountId;
            _secretKey = secretKey;
        }

        public IReadOnlyList<PaymentProvider> GetPaymentProviders()
        {
            IRestRequest request = CreateRequest("/merchants/payment-providers", Method.GET, null);

            var response = _client.Execute<List<PaymentProvider>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }

            throw new Exception("Something happened");
        }

        // TODO: This is not implemented yet on Checkout Finland new API
        public PaymentResponse GetTransactionInformation(Guid transactionId)
        {
            // todo: Create transaction request
            if (transactionId == null || transactionId == Guid.Empty) throw new Exception("Invalid transactionId provided");

            var extraHeader = new Dictionary<string, string>() {
                { CheckoutRequestHeaders.TransactionId, transactionId.ToString() }
            };

            IRestRequest request = CreateRequest("/payments/{transactionId}", Method.GET, null, extraHeader)
                .AddParameter("transactionId", transactionId, ParameterType.UrlSegment);
            // End of create transaction request

            var response = _client.Execute<PaymentResponse>(request);
            // TODO: Response handling
            return response.Data;
        }

        public IRestResponse CreateRefund()
        {
            throw new NotImplementedException();
        }

        // TODO: Exception handling
        public CreatePaymentResponse CreatePayment(CreatePaymentRequestBody createRequestBody)
        {
            IRestRequest request = CreateRequest("/payments/", Method.POST, createRequestBody);
            var response = _client.Execute<CreatePaymentResponse>(request);

            return response.Data;
        }

        // TODO: This is ugly af , maybe an Action<request,header> instead ?
        // <summary>
        // Create IRestRequest object with required headers for Checkout API
        // </summary>
        private IRestRequest CreateRequest(string url, Method method, object requestBody, IDictionary<string, string> extraHeader = null)
        {
            IRestRequest request = new RestRequest(url, method, DataFormat.Json);

            IDictionary<string, string> headerDictionary = new Dictionary<string, string>()
            {
                { CheckoutRequestHeaders.Method, method.ToString("g") },
                { CheckoutRequestHeaders.NOnce, Guid.NewGuid().ToString() },
                { CheckoutRequestHeaders.Timestamp, DateTimeOffset.UtcNow.ToIsoDateTimeString() }
            };

            // TODO: If extra header already there , wut wut ?
            if (extraHeader != null) extraHeader.ToList().Each(kv => { headerDictionary.Add(kv.Key, kv.Value); });

            headerDictionary.ToList().Each(kv => { request.AddHeader(kv.Key, kv.Value); });

            var toEncrypt = EncryptionUtils.ConvertCustomRequestHeaders(
                    defaultHeaders.Concat(headerDictionary).ToDictionary(x => x.Key, x => x.Value)
                    );

            var signature = EncryptionUtils.CalculateHmac(_secretKey, toEncrypt.ToList(), SerializationUtils.RequestBodyToString(requestBody));

            request.AddHeader(CheckoutRequestHeaders.Signature, signature.ToLower());
            if (requestBody != null) request.AddJsonBody(requestBody);

            return request;
        }

    }
}

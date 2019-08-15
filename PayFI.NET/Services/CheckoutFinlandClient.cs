using Baseline;
using PayFI.NET.Library.Model.CheckoutFinland;
using PayFI.NET.Model.CheckoutFinland.Payment;
using PayFI.NET.Model.Merchant;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PayFI.NET
{
    public class CheckoutRequestHeaders
    {
        public static readonly string Account = "checkout-account";
        public static readonly string Algorithm = "checkout-algorithm";
        public static readonly string Method = "checkout-method";
        public static readonly string NOnce = "checkout-nonce";
        public static readonly string Timestamp = "checkout-timestamp";

        public static readonly string Signature = "signature";

    }

    public class CheckoutFinlandClient
    {
        const string ApiUrl = "https://api.checkout.fi";
        private readonly IRestClient _client;

        private readonly IReadOnlyList<string> checkoutPrequisiteHeaders = new List<string>()
        {
            CheckoutRequestHeaders.Account, CheckoutRequestHeaders.Algorithm, CheckoutRequestHeaders.Method, CheckoutRequestHeaders.NOnce, CheckoutRequestHeaders.Timestamp
        };

        // TODO: No , not here, somewhere else please
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

        public IEnumerable<PaymentProvider> GetPaymentProviders()
        {
            IRestRequest request = CreateRequest("/merchants/payment-providers", Method.GET, null);

            var response = _client.Execute<List<PaymentProvider>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }

            throw new Exception("Something happened");
        }

        public IRestResponse GetPayment()
        {

            IRestRequest request = CreateRequest("/payments/0fbda2ce-8115-11e8-a3c2-1b42d60c4148", Method.GET, null);
            var response = _client.Execute(request);

            return response;
        }

        public IRestResponse CreatePayment()
        {
            CreatePaymentRequestBody requestBody = new CreatePaymentRequestBody()
            {
                Stamp = Guid.NewGuid().ToString(),
                Reference = Guid.NewGuid().ToString(),
                Amount = 1000,
                Currency = "EUR",
                Language = "EN",
                Customer = new Customer()
                {
                    Email = "test@gmail.com",
                    FirstName = "Tri",
                    LastName = "Helen"
                },
                DeliveryAddress = new Address()
                {
                    City = "Helsinki",
                    Country = ""
                }
            };

            IRestRequest request = CreateRequest("/payments/", Method.POST, requestBody);
            var response = _client.Execute(request);

            return response;
        }

        private IRestRequest CreateRequest(string url, Method method, object requestBody)
        {
            IRestRequest request = new RestRequest(url, method, DataFormat.Json);

            IDictionary<string, string> headerDictionary = new Dictionary<string, string>()
            {
                { CheckoutRequestHeaders.Method, method.ToString("g") },
                { CheckoutRequestHeaders.NOnce, Guid.NewGuid().ToString() },
                { CheckoutRequestHeaders.Timestamp, DateTimeOffset.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture) }
            };

            headerDictionary.ToList().Each(kv => { request.AddHeader(kv.Key, kv.Value); });

            var toEncrypt = EncryptionUtils.ConvertCustomRequestHeaders(
                    defaultHeaders.Concat(headerDictionary).ToDictionary(x => x.Key, x => x.Value)
                    );

            var signature = EncryptionUtils.CalculateHmac(_secretKey, toEncrypt.ToList(),
                    requestBody != null ? EncryptionUtils.RequestBodyToString(requestBody) : string.Empty);

            request.AddHeader(CheckoutRequestHeaders.Signature, signature.ToLower());
            if (requestBody != null) request.AddJsonBody(requestBody);

            return request;
        }

    }
}

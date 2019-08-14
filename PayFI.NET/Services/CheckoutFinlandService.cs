using PayFI.NET.Library.Model.CheckoutFinland;
using PayFI.NET.Model.Merchant;
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

    public class CheckoutFinlandService
    {
        private readonly HttpClient _httpClient;
        private readonly IReadOnlyList<string> checkoutPrequisiteHeaders = new List<string>()
        {
            CheckoutRequestHeaders.Account, CheckoutRequestHeaders.Algorithm, CheckoutRequestHeaders.Method, CheckoutRequestHeaders.NOnce, CheckoutRequestHeaders.Timestamp
        };

        // TODO: No , not here, somewhere else please
        private readonly string secretKey = "SAIPPUAKAUPPIAS";


        // Let's just have consumer use their own HttpClient implementation
        public CheckoutFinlandService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://checkoutfinland.org");

            // Only use SHA256 for now
            SetRequestHeader(CheckoutRequestHeaders.Algorithm, "SHA256");

            _httpClient = httpClient;
        }

        // TODO: Signature method should be reset for these header changes
        public CheckoutFinlandService SetAccountID(string accountId)
        {
            SetRequestHeader(CheckoutRequestHeaders.Account, accountId);
            ResetSignatureHeader();
            return this;
        }

        public CheckoutFinlandService SetRequestMethod(HttpMethod method)
        {
            SetRequestHeader(CheckoutRequestHeaders.Method, method.Method.ToUpper());
            ResetSignatureHeader();
            return this;
        }

        public CheckoutFinlandService SetNOnce(string nOnce)
        {
            SetRequestHeader(CheckoutRequestHeaders.NOnce, nOnce);
            ResetSignatureHeader();
            return this;
        }

        public CheckoutFinlandService SetTimestamp(DateTimeOffset timestamp)
        {
            SetRequestHeader(CheckoutRequestHeaders.Timestamp, timestamp.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
            ResetSignatureHeader();
            return this;
        }
        
        // TODO : to getter?
        private IDictionary<string,string> GetAllCheckoutFinlandHeaders()
        {
            return checkoutPrequisiteHeaders.ToDictionary(x => _httpClient.DefaultRequestHeaders.GetValues(x).FirstOrDefault());
        }

        private CheckoutFinlandService SetSignature(string jsonBody)
        {
            // TODO: If no required headers found , throw
            var signature = EncryptionUtils.CalculateHmac(secretKey, GetAllCheckoutFinlandHeaders(), jsonBody);
            SetRequestHeader(CheckoutRequestHeaders.Signature, signature);
            return this;
        }

        public async Task<IEnumerable<PaymentProvider>> GetAllPaymentProvider()
        {
            return Array.Empty<PaymentProvider>();
        }

        private void ResetSignatureHeader()
        {
            if (_httpClient.DefaultRequestHeaders.Contains(CheckoutRequestHeaders.Signature))
            {
                _httpClient.DefaultRequestHeaders.Remove(CheckoutRequestHeaders.Signature);
            }
        }

        private void CheckHasSignature()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains(CheckoutRequestHeaders.Signature)) throw new Exception($"Header {CheckoutRequestHeaders.Signature} not defined for request!");
        }

        private CheckoutFinlandService SetRequestHeader(string headerName, string headerValue)
        {
            if (_httpClient.DefaultRequestHeaders.Contains(headerName))
            {
                _httpClient.DefaultRequestHeaders.Remove(headerName);
            }
            _httpClient.DefaultRequestHeaders.Add(headerName, headerValue);

            return this;
        }


        public async Task<object> DoSomething()
        {
            throw new NotImplementedException();
        }
    }
}

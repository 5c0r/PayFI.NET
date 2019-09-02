using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace PayFI.NET.Library.Model.CheckoutFinland
{
    public sealed class EncryptionUtils
    {
        public static IEnumerable<string> ConvertCustomRequestHeaders(IDictionary<string, string> requestHeaders)
        {
            return requestHeaders
                .Where(x => x.Key.StartsWith("checkout-"))
                .OrderBy(x => x.Key)
                .Select(x => $"{x.Key}:{x.Value}")
                .ToList();
        }

        public static string CalculateHmac(string secretKey, IDictionary<string, string> requestHeaders, string jsonBody)
        {
            byte[] secretByte = Encoding.UTF8.GetBytes(secretKey);

            IEnumerable<string> convertedHeaders = ConvertCustomRequestHeaders(requestHeaders).Concat(new List<string>() { jsonBody });

            // TODO: Should it be Environment.Newline ?
            byte[] requestPayload = Encoding.UTF8.GetBytes(string.Join("\n", convertedHeaders ));

            using (HMACSHA256 hmac = new HMACSHA256(secretByte))
            {
                var byteResult = hmac.ComputeHash(requestPayload);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in byteResult)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        public static string CalculateHmac(string secretKey, IEnumerable<string> headers, string jsonBody)
        {
            byte[] secretByte = Encoding.UTF8.GetBytes(secretKey);

            IEnumerable<string> convertedHeaders = headers.Concat(new List<string>() { jsonBody });

            // TODO: Should it be Environment.Newline ?
            byte[] requestPayload = Encoding.UTF8.GetBytes(string.Join("\n", convertedHeaders));

            using (HMACSHA256 hmac = new HMACSHA256(secretByte))
            {
                var byteResult = hmac.ComputeHash(requestPayload);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in byteResult)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}

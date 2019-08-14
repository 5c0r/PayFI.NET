using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using Newtonsoft.Json;

namespace PayFI.NET.Library.Model.CheckoutFinland
{
    public sealed class EncryptionUtils
    {
        public static string RequestBodyToString(object anything)
        {
            // TODO: If null ?
            return JsonConvert.SerializeObject(anything).ToString();
        }

        public static IEnumerable<string> ConvertCustomRequestHeaders(IDictionary<string, string> requestHeaders)
        {
            return requestHeaders
                .OrderBy(x => x.Key)
                .Select(x => $"{x.Key}:{x.Value}")
                .ToList();
        }

        public static string CalculateHmac(string secretKey, IDictionary<string, string> requestHeaders, string jsonBody)
        {
            byte[] secretByte = Encoding.ASCII.GetBytes(secretKey);

            IEnumerable<string> convertedHeaders = ConvertCustomRequestHeaders(requestHeaders).Concat(new List<string>() { jsonBody });

            // TODO: Should it be Environment.Newline ?
            byte[] requestPayload = Encoding.ASCII.GetBytes(string.Join("\n", convertedHeaders ));

            using (HMACSHA256 hmac = new HMACSHA256(secretByte))
            {
                var byteResult = hmac.ComputeHash(requestPayload);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in byteResult)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}

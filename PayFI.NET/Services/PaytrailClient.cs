using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Services
{
    public sealed class PaytrailRequestHeaders
    {

    }

    public sealed class PaytrailClient
    {
        private readonly string ServiceUrl = string.Empty;
        private readonly string MerchantId = string.Empty;
        private readonly string MerchantSecret = string.Empty;

        private readonly IRestClient _client;

        public PaytrailClient(string merchantId, string merchantSecret, string serviceUrl = "https://payment.paytrail.com")
        {
            MerchantId = merchantId;
            MerchantSecret = merchantSecret;
            ServiceUrl = serviceUrl;

            // TODO: Serializer ? Which?
            // TODO: Add header
            _client = new RestClient(ServiceUrl);
                //.AddDefaultHeader("")

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MerchantWeb.Provider
{
    public sealed class PaymentService
    {
        private readonly IHttpClientFactory _clientFactory;

        public PaymentService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task RequestSomething()
        {

            return null;
        }

    }
}

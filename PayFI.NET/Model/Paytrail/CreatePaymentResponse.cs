using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.Paytrail
{
    public sealed class CreatePaymentResponse
    {
        public string OrderNumber { get; set; }
        public string Token { get; set; }
        public string Url { get; set; }

    }
}

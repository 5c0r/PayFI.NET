using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.Paytrail
{
    public sealed class CreatePaymentRequest
    {
        public string OrderNumber { get; set; }
        public int ReferenceNumber { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Locale { get; set; }
        public object UrlSet { get; set; }
        public object OrderDetails { get; set; }
        public double Price { get; set; }
    }
}

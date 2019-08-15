using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class PaymentResponse
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public long Stamp { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
    }
}

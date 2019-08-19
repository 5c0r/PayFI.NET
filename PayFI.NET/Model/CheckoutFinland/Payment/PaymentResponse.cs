using System;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class PaymentResponse : IHaveStamp
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public Guid Stamp { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
    }
}

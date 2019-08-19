using System;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public class PaymentRequest : IHaveReference, IHaveStamp
    {
        public string Reference { get; set; }

        public Guid Stamp { get; set; }
    }
}

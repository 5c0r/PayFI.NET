using PayFI.NET.Model.Merchant;
using System;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class CreatePaymentResponse
    {
        public Guid TransactionId { get; set; }
        public string Href { get; set; }
        public PaymentProvider[] Providers { get; set; }
    }
}

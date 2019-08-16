using PayFI.NET.Model.Merchant;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class CreatePaymentResponse
    {
        public string TransactionId { get; set; }
        public string Href { get; set; }

        public PaymentProvider[] Providers { get; set; }
    }
}

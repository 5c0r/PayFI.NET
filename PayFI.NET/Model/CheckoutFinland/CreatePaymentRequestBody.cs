using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayFI.NET.Library.Model.CheckoutFinland.Payment;

namespace PayFI.NET.Library.Model.CheckoutFinland
{
    // TODO: Request body builder ?!?!?!?
    public sealed class CreatePaymentRequestBody : PaymentRequest
    {
        public string Stamp { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string OrderId { get; set; }
        public Item[] Items { get; set; }
        public Customer Customer { get; set; }
        public Address DeliveryAddress { get; set; }
        public Address InvoicingAddress { get; set; }

        public CallbackUrl RedirectUrls { get; set; }
        public CallbackUrl CallbackUrls { get; set; }
    }
}

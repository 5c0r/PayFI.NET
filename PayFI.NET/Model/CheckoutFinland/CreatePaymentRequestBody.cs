using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayFI.NET.Model.CheckoutFinland.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland
{
    // Maybe NOT...
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public sealed class CreatePaymentRequestBody
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

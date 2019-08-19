using Newtonsoft.Json;
using System;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class Item : IHaveReference, IHaveStamp
    {
        public double UnitPrice { get; set; }
        public uint Units { get; set; }
        public double VatPercentage { get; set; }
        public string ProductCode { get; set; }
        public string DeliveryDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        // TODO: Guid or stamp?
        public Guid Stamp { get; set; }
        public string Reference { get; set; }
        public string Merchant { get; set; }

        // This was needed in documentation ?!
        [JsonIgnore]
        public object Commission { get; set; }
        [JsonIgnore]
        public string OrderId { get; set; }
    }
}

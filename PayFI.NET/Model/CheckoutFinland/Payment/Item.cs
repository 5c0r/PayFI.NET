using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class Item
    {
        public double UnitPrice { get; set; }
        public uint Units { get; set; }
        public double VatPercentage { get; set; }
        public string ProductCode { get; set; }
        public string DeliveryDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [JsonIgnore]
        public string OrderId { get; set; }
        // TODO: Guid or stamp?
        public string Stamp { get; set; }
        public string Reference { get; set; }
        public string Merchant { get; set; }
        [JsonIgnore]
        public object Commission { get; set; }
    }
}

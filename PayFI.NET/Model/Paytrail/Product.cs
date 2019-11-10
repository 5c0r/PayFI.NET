using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.Paytrail
{
    public enum ProductType
    {
        Normal = 1,
        Postal =2,
        Handling = 3
    }
    public sealed class Product
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double Vat { get; set; }
        public double Discount { get; set; }
        public ProductType Type { get; set; }
    }
}

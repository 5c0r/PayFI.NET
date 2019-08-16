using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class Address
    {
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
    }
}

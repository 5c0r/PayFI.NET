using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Model.CheckoutFinland.Payment
{
    public sealed class Customer
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string VatId { get; set; }
    }
}

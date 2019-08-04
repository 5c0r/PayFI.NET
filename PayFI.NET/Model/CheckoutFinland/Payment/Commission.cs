using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Model.CheckoutFinland.Payment
{
    public sealed class Commission
    {
        public string Merchant { get; set; }
        public double Amount { get; set; }
    }
}

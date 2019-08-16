using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public sealed class CallbackUrl
    {
        public string Success { get; set; }
        public string Cancel { get; set; }
    }
}

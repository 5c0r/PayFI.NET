using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public class RefundResponse
    {
        public string Provider { get; set; }
        public string Status { get; set; }
        public Guid TransactionId { get; set; }
    }
}

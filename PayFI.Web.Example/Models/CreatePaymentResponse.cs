using PayFI.NET.Library.Model.CheckoutFinland.Payment;
using PayFI.NET.Model.Merchant;
using System;
using System.Collections.Generic;

namespace PayFI.Web.Example.Models
{
    public sealed class CreatePaymentResponseModel : IHaveTransactionId
    {
        public double Total { get; set; }
        public IEnumerable<PaymentProvider> PaymentProviders { get; set; }

        public Guid TransactionId { get; set; }
        public string Href { get; set; }
    }
}

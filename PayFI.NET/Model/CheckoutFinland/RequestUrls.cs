using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland
{
    public class RequestTemplateUrls
    {
        public static readonly string GetPayments = "/payments";
        public static readonly string GetPayment = "/payments/{transactionId}";
        public static readonly string RefundPayment = "/payments/{transactionId}/refund";
        public static readonly string MerchantProviders = "/merchants/payment-providers";
        public static readonly string Settlements = "/settlements";

        // TODO: Payment reports . Later
    }
}

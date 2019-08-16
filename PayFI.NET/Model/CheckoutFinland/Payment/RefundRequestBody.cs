using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public class MerchantRefundRequestBody
    {
        public double Amount { get; set; }
        public CallbackUrl CallbackUrls { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }

    public class ShopInShopRefundRequestBody
    {
        public double Amount { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public CallbackUrl CallbackUrls { get; set; }
    }

    public class EmailRefundRequestBody
    {
        public double Amount { get; set; }
        public string Email { get; set; }
        public CallbackUrl CallbackUrls { get; set; }
    }
}

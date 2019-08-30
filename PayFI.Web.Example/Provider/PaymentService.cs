using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayFI.NET.Library.Model.CheckoutFinland;
using PayFI.NET.Library.Model.CheckoutFinland.Payment;
using PayFI.NET.Library.Services;
using PayFI.NET.Model.Merchant;
using PayFI.Web.Example.Models;

namespace PayFI.Web.Example.Provider
{
    public sealed class PaymentService
    {
        private readonly CheckoutFinlandClient checkoutClient;

        private readonly string accountId = "375917";

        public PaymentService()
        {
            // TODO: DI DI DI DI
            var secret = "SAIPPUAKAUPPIAS";
            checkoutClient = new CheckoutFinlandClient(accountId, secret);
        }

        public double CalculateTotal(IEnumerable<Item> items)
        {
            return items.Select(a => a.UnitPrice * a.Units).Aggregate((a, b) => a + b);
        }

        // TODO
        public CreatePaymentResponseModel GetPaymentProviders(IEnumerable<Item> items)
        {
            var createPaymentResponse = checkoutClient.CreatePayment(CreatePaymentRequest(items));
            return new CreatePaymentResponseModel()
            {
                TransactionId = createPaymentResponse.TransactionId,
                Href = createPaymentResponse.Href,
                Total = CalculateTotal(items),
                PaymentProviders = createPaymentResponse.Providers
            };
        }

        private CreatePaymentRequestBody CreatePaymentRequest(IEnumerable<Item> items)
        {
            var total = CalculateTotal(items);
            var orderId = Guid.NewGuid().ToString();
            var reference = Guid.NewGuid().ToString();
            var stamp = Guid.NewGuid();

            // TODO: Validation before really sending request

            CreatePaymentRequestBody paymentRequest = new CreatePaymentRequestBody()
            {
                OrderId = orderId,
                Stamp = stamp,
                Reference = reference,
                Amount = total,
                Currency = "EUR",
                Language = "EN",
                Customer = new Customer()
                {
                    Email = "test@gmail.com",
                    FirstName = "Tri",
                    LastName = "Helen",
                    Phone = "0401234123",
                    VatId = string.Empty
                },
                Items = items
                    .Select(x =>
                    {
                        x.Merchant = accountId;
                        x.Reference = reference;
                        x.Stamp = stamp;
                        x.ProductCode = Guid.NewGuid().ToString(); // TODO: Item repository
                        x.DeliveryDate = DateTimeOffset.Now.AddDays(10).ToCheckoutDateFormat();

                        return x;
                    }).ToArray(),
                DeliveryAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    StreetAddress = "Something else 2",
                    PostalCode = "01300",
                    County = string.Empty
                },
                // What is call back , what is redirect
                // TODO: Url/callback cannot be an IP address :(
                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://something.com/api/checkout/success",
                    Cancel = "https://something.com/api/checkout/cancel"
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://something.com/checkout-success",
                    Cancel = "https://something.com/checkout-cancel"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    StreetAddress = "Something 2",
                    PostalCode = "01300",
                    County = string.Empty
                },
            };

            return paymentRequest;
        }

    }
}

using FluentAssertions;
using PayFI.NET.Library.Model.CheckoutFinland;
using PayFI.NET.Library.Model.CheckoutFinland.Payment;
using PayFI.NET.Library.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace PayFI.NET.Tests
{
    public class CheckoutFinlandClientTests
    {
        private readonly string testAccount = "375917";
        private readonly string testSecretKey = "SAIPPUAKAUPPIAS";
        
        [Fact]
        public void CanFetchPaymentProviders()
        {
            var clientUnderTest = new CheckoutFinlandClient(testAccount, testSecretKey);

            var paymentProviders = clientUnderTest.GetPaymentProviders();

            paymentProviders.Count.Should().BeGreaterThan(0);
        }

        // TODO: This can be compiled into a BIG scenario
        [Fact]
        public void CanCreatePayment()
        {
            var clientUnderTest = new CheckoutFinlandClient(testAccount, testSecretKey);

            var orderId = Guid.NewGuid().ToString();
            var reference = Guid.NewGuid().ToString();
            var stamp = Guid.NewGuid();

            var itemList = new List<Item>()
            {
                new Item()
                {
                    Units = 1, UnitPrice = 100, VatPercentage = 24, ProductCode = Guid.NewGuid().ToString(), DeliveryDate = DateTimeOffset.Now.AddDays(10).ToCheckoutDateFormat(),

                    //OrderId = orderId, 
                    Stamp = stamp, Reference = reference, Merchant = testAccount, Description = "tEST", Category = "Test"
                }
            };

            CreatePaymentRequestBody requestBody = new CreatePaymentRequestBody()
            {
                OrderId = orderId,
                Stamp = stamp,
                Reference = reference,
                Amount = 100,
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
                Items = itemList.ToArray(),
                DeliveryAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    StreetAddress = "Atomi 2",
                    PostalCode = "01300",
                    County = string.Empty
                },
                // Should there be any transaction ID associated here ?
                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://something.com/success/",
                    Cancel = "https://something.com/cancel/"
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://something.com/success/",
                    Cancel = "https://something.com/cancel/"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    StreetAddress = "Atomi 2",
                    PostalCode = "01300",
                    County = string.Empty
                },
            };

            // TODO : stringId as param
            var paymentResponse = clientUnderTest.CreatePayment(requestBody);

            paymentResponse.Should().NotBeNull();
            paymentResponse.Href.Should().NotBeNullOrEmpty();
            paymentResponse.TransactionId.Should().NotBeEmpty();
            paymentResponse.Providers.Length.Should().BeGreaterThan(0);

            var paymentInformation = clientUnderTest.GetTransactionInformation(paymentResponse.TransactionId);
            paymentInformation.Should().NotBeNull();
            paymentInformation.Id.Should().NotBeEmpty();

            var refundRequest = new MerchantRefundRequestBody()
            {
                Amount = 100,
                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://something.com/success/",
                    Cancel = "https://something.com/cancel/"
                },
                Items = new List<Item>()
            };

            // TODO: Pay the damn transaction

            //var refund = clientUnderTest.CreateRefund(paymentResponse.TransactionId, refundRequest);
            //refund.TransactionId.Should().Be(paymentResponse.TransactionId);
            //refund.Status.Should().Be("0");
        }

    }
}

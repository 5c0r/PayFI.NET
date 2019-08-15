using System;
using System.Collections.Generic;
using System.Text;
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
        }

        //[Fact]
        //public void CanGetPayment()
        //{
        //    var clientUnderTest = new CheckoutFinlandClient(testAccount, testSecretKey);

        //    // TODO : stringId as param
        //    var paymentProviders = clientUnderTest.GetPayment();
        //}

        [Fact]
        public void CanCreatePayment()
        {
            var clientUnderTest = new CheckoutFinlandClient(testAccount, testSecretKey);

            // TODO : stringId as param
            var paymentProviders = clientUnderTest.CreatePayment();
        }

    }
}

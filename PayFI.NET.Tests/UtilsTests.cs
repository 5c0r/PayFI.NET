using FluentAssertions;
using PayFI.NET.Library.Model.CheckoutFinland;
using PayFI.NET.Library.Services;
using System.Collections.Generic;
using Xunit;


namespace PayFI.NET.Tests
{
    public class UtilsTests
    {
        [Fact]
        public void CanEncryptWithEmptyBody()
        {
            const string expected = "9ebf9b5ea57b6cbb26ffe539a0c52681e52f2c86e24c606ba042d972168b0dba";
            IDictionary<string, string> headersDictionary = new Dictionary<string, string>() {
                { CheckoutRequestHeaders.Account, "375917" },
                { CheckoutRequestHeaders.Algorithm, "sha256" },
                { CheckoutRequestHeaders.Method, "GET" },
                { CheckoutRequestHeaders.NOnce, "564635208570151" },
                { CheckoutRequestHeaders.Timestamp, "2018-07-06T10:01:31.904Z" }
            };

            var secretKey = "SAIPPUAKAUPPIAS";
            var jsonBody = string.Empty;

            var encryptedString = EncryptionUtils.CalculateHmac(secretKey, headersDictionary, jsonBody);
            var anotherEncryptedString = EncryptionUtils.CalculateHmac(secretKey, EncryptionUtils.ConvertCustomRequestHeaders(headersDictionary), jsonBody);

            encryptedString.Should().BeEquivalentTo(expected);
            anotherEncryptedString.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CanEncryptWithBody()
        {

        }
    }
}

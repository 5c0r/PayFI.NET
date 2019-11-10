using PayFI.NET.Library.Services;
using Xunit;
using FluentAssertions;

namespace PayFI.NET.Tests
{
    public sealed class PaytrailClientTests
    {
        private readonly string testMerchantId = string.Empty;
        private readonly string testMerchantSecret = string.Empty;

        [Fact]
        public void CanInit()
        {
            var clientUnderTest = new PaytrailClient(testMerchantId, testMerchantSecret);
            clientUnderTest.Should().NotBeNull();

        }
    }
}

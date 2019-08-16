namespace PayFI.NET.Model.Merchant
{
    public class PaymentProvider
    {
        public string Id { get; set; }
        public string Icon { get; set; }
        public string Svg { get; set; }
        public string Group { get; set; }
        public PaymentProviderRequestParameter[] Parameters { get; set; }

        public PaymentProvider()
        {

        }
    }

    public sealed class PaymentProviderRequestParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
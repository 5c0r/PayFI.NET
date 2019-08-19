using System;

namespace PayFI.NET.Library.Model.CheckoutFinland.Exceptions
{
    public class CheckoutFinlandException : System.Exception
    {
        public string CheckoutRequestId { get; private set; }

        public CheckoutFinlandException(string requestId) : base()
        {
            CheckoutRequestId = requestId;
        }

        public CheckoutFinlandException(string requestId, string message, Exception e ) : base(message, e)
        {
            CheckoutRequestId = requestId;
        }
    }
}

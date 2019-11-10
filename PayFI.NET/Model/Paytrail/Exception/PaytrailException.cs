using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.Paytrail.Exception
{
    public sealed class PaytrailException : System.Exception
    {
        public PaytrailException(string message) : base(message)
        {
        }

        public PaytrailException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}

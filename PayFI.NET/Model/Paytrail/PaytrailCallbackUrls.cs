using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.Paytrail
{
    public sealed class PaytrailCallbackUrls
    {
        public string Success { get; set; }
        public string Failure { get; set; }
        public string Pending { get; set; }
        public string Notification { get; set; }

    }
}

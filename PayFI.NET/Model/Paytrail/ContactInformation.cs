using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFI.NET.Library.Model.Paytrail
{
    public sealed class ContactInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PostalOffice { get; set; }
        public string Country { get; set; }
        [JsonProperty("telNo")]
        public string Telephone { get; set; }
        [JsonProperty("cellNo")]

        public string Cell { get; set; }
        public string Company { get; set; }
    }
}

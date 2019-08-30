using Newtonsoft.Json;
using System;

namespace PayFI.NET.Library.Model.CheckoutFinland
{
    public class SerializationUtils
    {
        public static string RequestBodyToString(object anything)
        {
            if (anything == null) return string.Empty;
            return JsonConvert.SerializeObject(anything, JsonNetSerializer.SerializerSettings).ToString();
        }

        public static T DeserializeResponse<T>(string anything) where T : class
        {
            if (string.IsNullOrEmpty(anything)) return null;
            return JsonConvert.DeserializeObject<T>(anything, JsonNetSerializer.SerializerSettings);
        }
    }

    public static class DateTimeOffsetExtensions
    {
        public static string ToIsoDateTimeString(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string ToCheckoutDateFormat(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("yyyy-MM-dd");
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serialization;

namespace PayFI.NET.Library.Model.CheckoutFinland
{
    internal class JsonNetSerializer : IRestSerializer
    {
        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public JsonNetSerializer()
        {
        }

        public string Serialize(object obj) =>
            JsonConvert.SerializeObject(obj, SerializerSettings);

        public string Serialize(Parameter bodyParameter) =>
            JsonConvert.SerializeObject(bodyParameter.Value, SerializerSettings);

        // TODO
        public T Deserialize<T>(IRestResponse response) =>
            JsonConvert.DeserializeObject<T>(response.Content);

        public string[] SupportedContentTypes { get; } =
        {
                "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;
    }
}

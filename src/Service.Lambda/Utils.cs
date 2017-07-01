namespace SerialNumber.Service.Lambda
{
    using System.IO;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class Utils
    {
        public static MemoryStream ToJsonMemoryStream(object o)
        {
            var serialiser = new JsonSerializer()
                                 {
                                     DefaultValueHandling = DefaultValueHandling.Ignore,
                                     ContractResolver = new CamelCasePropertyNamesContractResolver()
                                 };

            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                serialiser.Serialize(writer, o);
                writer.Flush();
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(stringWriter.ToString()));
        }

        public static T Bind<T>(Stream stream)
        {
            var serialiser = new JsonSerializer() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            T thingShadow;
            using (var sr = new StreamReader(stream))
            {
                thingShadow = serialiser.Deserialize<T>(new JsonTextReader(sr));
            }

            return thingShadow;
        }
    }
}

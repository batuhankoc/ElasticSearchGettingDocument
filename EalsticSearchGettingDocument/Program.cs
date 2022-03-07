using Elasticsearch.Net;
using Newtonsoft.Json.Linq;

namespace ElasticSearchGettingDocument
{
    public class Program
    {
        public static void Main(string[] args)

        {
            string fieldName = "field name";
            string value = "value";
            string index = "index name";
            string uri = "your elasticsearch uri";

            var settings = new ConnectionConfiguration(new Uri(uri))
            .RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient = new ElasticLowLevelClient(settings);

            JTokenWriter writer = new JTokenWriter();
            writer.WriteStartObject();
            writer.WritePropertyName("from");
            writer.WriteValue(0);
            writer.WritePropertyName("size");
            writer.WriteValue(10);
            writer.WritePropertyName("query");
            writer.WriteStartObject();
            writer.WritePropertyName("match");
            writer.WriteStartObject();
            writer.WritePropertyName(fieldName);
            writer.WriteStartObject();
            writer.WritePropertyName("query");
            writer.WriteValue(value);
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteEndObject();
            JObject product = (JObject)writer.Token;
            Console.WriteLine(product.ToString());

            var searchResponse = lowlevelClient.Search<StringResponse>(index, product.ToString());
            var successful = searchResponse.Success;
            var responseJson = searchResponse.Body;
            Console.WriteLine(responseJson.ToString());
            Console.WriteLine(successful);
        }
    }
}
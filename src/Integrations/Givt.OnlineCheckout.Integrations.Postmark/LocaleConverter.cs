using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Givt.OnlineCheckout.Integrations.Postmark
{

    /// <summary>
    /// This class helps to serialise a locale like "en-GB" to a structure that can be used in Postmark to localise emails.
    /// <para>Examples:</para>
    /// <list type="bullet">
    /// <item>Locale = "en-US" -> "en" : { "US" : true } </item>
    /// <item>Locale = "en" -> "en" : true </item>
    /// </list>
    /// </summary>
    public class LocaleConverter : JsonConverter
    {
        private readonly Type[] _types;

        public LocaleConverter(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);
            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                JProperty p = o.Property("Locale");
                if (p != null)
                {
                    var localeParts = p.Value.ToString().Split('-');
                    object data = true;
                    if (localeParts.Length > 1)
                    {
                        var child = new JObject
                        {
                            new JProperty(localeParts[1], true)
                        };
                        data = child;
                    }
                    o.AddFirst(new JProperty(localeParts[0], data));
                }
                o.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}

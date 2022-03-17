using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Givt.OnlineCheckout.Application.Models;
public class MediumIdTypeSerializer : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(MediumIdType?) || objectType == typeof(MediumIdType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        if (string.IsNullOrEmpty(token.ToString()))
            return null;
        MediumIdType c = token.ToString();
        return c;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var mediumIdType = (MediumIdType)value;
        serializer.Serialize(writer, (string)mediumIdType);
    }
}
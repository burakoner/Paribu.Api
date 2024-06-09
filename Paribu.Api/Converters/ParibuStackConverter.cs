namespace Paribu.Api.Converters;

internal class ParibuStackConverter<T> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(Dictionary<DateTime, double>));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var list = new List<T>();
        if (reader.TokenType == JsonToken.StartArray)
        {
            var ja = JArray.Load(reader);
            if (ja.Count == 0) return list;

            foreach (var item in ja)
            {
                var val = item.ToObject<T>(serializer);
                list.Add(val);
            }
        }

        if (reader.TokenType == JsonToken.StartObject)
        {
            Dictionary<string, T> dict = [];
            var jo = JObject.Load(reader);
            foreach (var jp in jo.Properties())
            {
                dict.Add(jp.Name, jp.Value.ToObject<T>(serializer));
            }

            list.AddRange(dict.Values);
        }

        return list;
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}

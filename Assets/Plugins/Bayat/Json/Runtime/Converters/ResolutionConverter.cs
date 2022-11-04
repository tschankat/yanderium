using System;
using System.Linq;
using Bayat.Json.Linq;
using UnityEngine;

using Bayat.Json.Serialization;

namespace Bayat.Json.Converters
{
    public class ResolutionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializerWriter internalWriter)
        {
            var res = (Resolution)value;
            writer.WriteStartObject();
            writer.WritePropertyName("height");
            writer.WriteValue(res.height);
            writer.WritePropertyName("width");
            writer.WriteValue(res.width);
            writer.WritePropertyName("refreshRate");
            writer.WriteValue(res.refreshRate);
            writer.WriteEndObject();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Resolution);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializerReader internalReader)
        {
            var obj = JObject.Load(reader);

            var result = new Resolution
            {
                height = (int)obj["height"],
                width = (int)obj["width"],
                refreshRate = (int)obj["refreshRate"]
            };

            return result;
        }

        public override bool CanRead
        {
            get { return true; }
        }
    }
}

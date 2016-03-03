using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageComposer
{
    public class RepoIdJsonConverter:JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RepoId);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                return null;
            }

            return new RepoId((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((RepoId)value).ToString());
        }

        public override bool CanWrite => true;
    }
}

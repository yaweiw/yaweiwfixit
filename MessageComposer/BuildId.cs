namespace MessageComposer
{
    using System;
    using System.Globalization;

    using Newtonsoft.Json;

    [JsonConverter(typeof(BuildIdJsonConverter))]
    public class BuildId : IComparable<BuildId>
    {
        private readonly string _id;

        #region Constructors

        public BuildId(string id)
        {
            ValidateId(id);
            _id = id;
        }

        public BuildId(DateTime dateTime, string branchName)
        {
            _id = string.Format("{0}-{1}", dateTime.ToString("yyyyMMddHHmmssffff"), branchName);
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var target = obj as BuildId;
            if (target == null)
            {
                return false;
            }

            return string.Equals(_id, target._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public int CompareTo(BuildId other)
        {
            if (other == null)
            {
                return 1;
            }

            return _id.CompareTo(other._id);
        }

        public override string ToString()
        {
            return _id;
        }

        public static explicit operator string(BuildId id)
        {
            return id?.ToString();
        }

        public static explicit operator BuildId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return new BuildId(id);
        }

        #endregion

        #region Private Methods

        private static void ValidateId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "id is null or empty");
            }

            int index = id.IndexOf('-');
            if (index > -1)
            {
                string dateTimePart = id.Substring(0, index);
                string branchPart = id.Substring(index + 1);

                DateTime dt;
                if (DateTime.TryParseExact(dateTimePart, "yyyyMMddHHmmssffff", new CultureInfo("en-US"), DateTimeStyles.None, out dt)
                    && branchPart.Length > 0)
                {
                    return;
                }
            }

            throw new ArgumentException($"Id {id} doesn't fit the right format 'timestamp-branch', the format of timestamp is 'yyyMMddHHmmssffff'.");
        }

        #endregion
    }

    internal class BuildIdJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BuildId);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                return null;
            }

            return new BuildId((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((BuildId)value).ToString());
        }

        public override bool CanWrite => true;
    }
}

using AndcultureCode.CSharp.Sitefinity.Core.Enumerations;
using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.JsonSerialization.Converters
{
    /// <summary>
    /// Responsible for converting RedirectPage enums values into numeric values
    /// </summary>
    public class RedirectPageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RedirectPage);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value != null ? Enum.Parse(typeof(RedirectPage), reader.Value.ToString()) : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long convertedValue = ((RedirectPage)value).GetHashCode();

            writer.WriteValue(convertedValue.ToString("d"));
        }
    }
}

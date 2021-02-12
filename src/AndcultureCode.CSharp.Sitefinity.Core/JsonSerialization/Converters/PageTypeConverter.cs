using AndcultureCode.CSharp.Sitefinity.Core.Enumerations;
using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.JsonSerialization.Converters
{
    /// <summary>
    /// Responsible for converting PageType enums values into numeric values
    /// </summary>
    public class PageTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PageType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value != null ? Enum.Parse(typeof(PageType), reader.Value.ToString()) : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long convertedValue = ((PageType)value).GetHashCode();

            writer.WriteValue(convertedValue.ToString("d"));
        }
    }
}

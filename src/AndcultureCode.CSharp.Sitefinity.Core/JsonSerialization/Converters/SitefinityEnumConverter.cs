using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.JsonSerialization.Converters
{
    /// <summary>
    /// Responsible for generically converting enums into values that correspond with how Sitefinity handles
    /// ChoiceField indices. See https://www.progress.com/documentation/sitefinity-cms/choicefield-operations
    /// for more information on how the ChoiceField works through the OData web services.
    ///
    /// Developer Note: This converter operates under the assumption that the specified Enum has values starting
    /// at 1 NOT 0.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SitefinityEnumConverter<T> : JsonConverter where T : struct, IConvertible
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            writer.WriteValue(((T)value).GetHashCode().ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (reader.Value == null)
            {
                return null;
            }

            return Enum.Parse(typeof(T), reader.Value.ToString());
        }


        public override bool CanConvert(Type objectType)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return objectType == typeof(T);
        }
    }
}

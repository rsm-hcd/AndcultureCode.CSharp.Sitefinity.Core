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

            long convertedValue = ((T) value).GetHashCode();

            writer.WriteValue(Math.Pow(2, convertedValue - 1).ToString());
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

            long convertedValue = Convert.ToInt64(reader.Value);

            // A zero is returned from Sitefinity when the value is not valid or null.
            if (convertedValue == 0)
            {
                return null;
            }

            long sitefinityValue = (long) (Math.Log(convertedValue, 2) + 1);

            return Enum.Parse(typeof(T), sitefinityValue.ToString());
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

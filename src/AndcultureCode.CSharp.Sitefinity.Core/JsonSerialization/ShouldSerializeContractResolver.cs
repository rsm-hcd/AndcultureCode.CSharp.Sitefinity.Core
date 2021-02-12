using AndcultureCode.CSharp.Sitefinity.Core.Models.Content;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace AndcultureCode.CSharp.Sitefinity.Core.JsonSerialization
{
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(SitefinityContent) && property.PropertyName == "Id")
            {
                property.ShouldSerialize = instance => false;
            }

            return property;
        }
    }
}

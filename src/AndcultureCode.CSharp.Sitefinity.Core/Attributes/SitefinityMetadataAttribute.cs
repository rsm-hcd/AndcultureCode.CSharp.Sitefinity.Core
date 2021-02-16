using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SitefinityMetadataAttribute : Attribute
    {
        public string DynamicContentType { get; }
        public string ParentDynamicContentType { get; }

        public SitefinityMetadataAttribute(string dynamicContentType, string parentDynamicContentType = null)
        {
            DynamicContentType = dynamicContentType;
            ParentDynamicContentType = parentDynamicContentType;
        }
    }
}

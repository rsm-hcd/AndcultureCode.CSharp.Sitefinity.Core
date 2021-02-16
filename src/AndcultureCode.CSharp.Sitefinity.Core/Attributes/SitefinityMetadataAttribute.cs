﻿using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SitefinityMetadataAttribute : Attribute
    {
        /// <summary>
        /// Represents the fully qualified object name of the model generated by Sitefinity when creating a dynamic content type. Refers to
        /// the fully qualified object name of the object this attribute is associated with.
        /// </summary>
        public string DynamicContentType { get; }

        /// <summary>
        /// Represents the fully qualified object name of the model generated by Sitefinity when creating a dynamic content type. Refers to
        /// the fully qualified object name of the object's parent (if one exists) that this attribute is associated with.
        /// </summary>
        public string ParentDynamicContentType { get; }

        public SitefinityMetadataAttribute(string dynamicContentType, string parentDynamicContentType = null)
        {
            DynamicContentType = dynamicContentType;
            ParentDynamicContentType = parentDynamicContentType;
        }
    }
}

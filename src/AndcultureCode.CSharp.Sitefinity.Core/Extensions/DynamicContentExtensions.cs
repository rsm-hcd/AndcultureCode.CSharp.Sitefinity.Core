using AndcultureCode.CSharp.Extensions;
using AndcultureCode.CSharp.Sitefinity.Core.Attributes;
using AndcultureCode.CSharp.Sitefinity.Core.Constants;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace AndcultureCode.CSharp.Sitefinity.Core.Extensions
{
    public static class DynamicContentExtensions
    {
        #region Public Properties

        public const char INCLUDE_PROPS_PATH_SEPARATOR = ',';
        public const char INCLUDE_PROPS_ITEM_SEPARATOR = '.';

        #endregion Public Properties

        #region Public Methods

        // This overload is used for LINQ expressions when we don't want to include
        // anything, as you can't have optional params in LINQ expressions.
        public static T MapTo<T>(this DynamicContent content) where T: new() => content.MapTo<T>(null);

        /// <summary>
        /// Maps a DynamicContent object to a custom model representing that dynamic content.
        /// This simplifies the conversion from a Sitefinity DynamicContent object into something
        /// more easily usable in code.
        /// </summary>
        /// <param name="content">The DynamicContent object to be mapped.</param>
        /// <param name="includes">A comma separated list of related objects to additionally map,
        /// with each include path separated by a comma and each property in a path separated by a period.
        /// E.g. "Buyer.Product,Category.ParentCategory"</param>
        /// <typeparam name="T">The type to which the DynamicContent object should be mapped.</typeparam>
        /// <returns>An instantiated object of type T, with the appropriate fields mapped from the DynamicContent object.</returns>
        public static T MapTo<T>(this DynamicContent content, string includes = null) where T: new()
        {
            var includePaths = includes?.Split(INCLUDE_PROPS_PATH_SEPARATOR).ToList() ?? new List<string>();

            if (content == null)
            {
                throw new ArgumentNullException();
            }

            var mappedItem = new T();

            var properties = mappedItem.GetType().GetProperties();
            var dynamicContentProperties = content.GetType().GetProperties();

            foreach (var property in properties)
            {
                // Check if this property has a relation attribute, if it does, map by getting the related value.
                var attributes = property.GetCustomAttributes(false);

                var relatedIncludePath = includePaths.FirstOrDefault(e => e.Split(INCLUDE_PROPS_ITEM_SEPARATOR).FirstOrDefault(p => p == property.Name) != null);

                if (relatedIncludePath != null)
                {
                    // Remove the first item in the include path as we pass it down the chain.
                    var newIncludePath = relatedIncludePath.Split(INCLUDE_PROPS_ITEM_SEPARATOR).Skip(1).Join(INCLUDE_PROPS_PATH_SEPARATOR.ToString());

                    var relatedContent = GetRelatedContent(content, property, newIncludePath);

                    property.SetValue(mappedItem, relatedContent);

                    continue;
                }

                // This property doesn't specify it comes from a relation, so map directly from the
                // dynamic content property with a matching name and type.
                var dynamicProperty = dynamicContentProperties.FirstOrDefault(e => e.Name == property.Name);

                if (dynamicProperty == null || dynamicProperty.PropertyType != property.PropertyType)
                {
                    continue;
                }

                property.SetValue(mappedItem, dynamicProperty.GetValue(content));
            }

            return mappedItem;
        }

        public static void SetPropertyInDataItem<T>(
            this T item,
            SitefinityMetadataAttribute metadataAttribute,
            ref DynamicContent dataItem,
            PropertyInfo property
        )
        {
            if (!property.PropertyType.IsSubclassOf(typeof(SitefinityContent)) &&
                !property.PropertyType.IsSubclassOf(typeof(Content)))
            {
                dataItem.SetValue(property.Name, item.GetType().GetProperty(property.Name)?.GetValue(item));
            }
            else if (property.PropertyType.IsSubclassOf(typeof(SitefinityContent)))
            {
                dataItem = SetPropertyInDataItemAsRelationship(
                    item,
                    metadataAttribute,
                    property,
                    dataItem
                );
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static object GetRelatedContent(DynamicContent content, PropertyInfo property, string includeString)
        {
            var relatedContent = content.GetRelatedItems(property.PropertyType.Name)?.Cast<DynamicContent>()?.FirstOrDefault();

            // We can't call a generic with a runtime type, so we need to use reflection.
            var mapMethod = typeof(DynamicContentExtensions).GetMethod(nameof(DynamicContentExtensions.MapTo), new [] { typeof(DynamicContent), typeof(string) });
            var generic = mapMethod.MakeGenericMethod(property.PropertyType);

            // Map the related content in case the related value ALSO needs to grab related content.
            var relatedContentMapped = generic.Invoke(null, new object[] { relatedContent, includeString });

            return relatedContentMapped;
        }

        private static DynamicContent SetPropertyInDataItemAsRelationship<T>(
            T item,
            SitefinityMetadataAttribute metadataAttribute,
            PropertyInfo property,
            DynamicContent dataItem
        )
        {
            var parentDynamicContentType = metadataAttribute.ParentDynamicContentType;
            var sitefinityContentItem = (SitefinityContent)item.GetType().GetProperty(property.Name)?.GetValue(item);

            SitefinityMetadataAttribute sitefinityType = (SitefinityMetadataAttribute)sitefinityContentItem?.GetType()
                .GetCustomAttribute(typeof(SitefinityMetadataAttribute));

            if (sitefinityType != null)
            {
                var relatedDynamicContentType = sitefinityType.DynamicContentType;

                var contentItemType = TypeResolutionService.ResolveType(relatedDynamicContentType.ToString());

                var dynamicModuleManager = DynamicModuleManager.GetManager(ProviderConstants.OpenAccessProvider);
                var dynamicContentItem = dynamicModuleManager.GetDataItem(contentItemType, sitefinityContentItem.Id);

                // If this property happens to be the Parent Relation, set the parent + SystemParentId. Otherwise, create the relation
                if (parentDynamicContentType != null && relatedDynamicContentType.Equals(parentDynamicContentType))
                {
                    dataItem.SetParent(dynamicContentItem);
                    dataItem.SystemParentId = dynamicContentItem.Id;

                    return dataItem;
                }

                dataItem.CreateRelation(dynamicContentItem, property.Name);
            }

            return dataItem;
        }

        #endregion Private Methods
    }
}

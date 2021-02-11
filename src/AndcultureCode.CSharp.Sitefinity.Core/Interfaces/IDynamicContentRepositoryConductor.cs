using AndcultureCode.CSharp.Core.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace AndcultureCode.CSharp.Sitefinity.Core.Interfaces
{
    public interface IDynamicContentRepositoryConductor
    {
        /// <summary>
        /// The provider name used by default to get dynamic content.
        /// </summary>
        string DefaultProviderName { get; set; }

        /// <summary>
        /// Gets live content from the dynamic module manager by type, and optionally applies a filter.
        /// </summary>
        /// <param name="contentType">String representation of the content type.</param>
        /// <param name="filter">Optional filter to apply to result set.</param>
        /// <returns></returns>
        IResult<IQueryable<DynamicContent>> GetLiveContentByType(string contentType, Expression<Func<DynamicContent, bool>> filter = null);

        /// <summary>
        /// Gets content from the dynamic module manager by type, and optionally applies a filter.
        /// </summary>
        /// <param name="contentType">String representation of the content type.</param>
        /// <param name="status">Status of the content you want returned. Null returns content in all statuses.</param>
        /// <param name="filter">Optional filter to apply to result set.</param>
        /// <returns></returns>
        IResult<IQueryable<DynamicContent>> GetContentByType(
            string contentType,
            ContentLifecycleStatus? status,
            Expression<Func<DynamicContent, bool>> filter = null);
    }
}

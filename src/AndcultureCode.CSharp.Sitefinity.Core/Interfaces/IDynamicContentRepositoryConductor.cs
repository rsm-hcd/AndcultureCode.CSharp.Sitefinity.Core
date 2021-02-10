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
        string DefaultProviderName { get; set; }

        IResult<IQueryable<DynamicContent>> GetLiveContentByType(string contentType, Expression<Func<DynamicContent, bool>> filter = null);
        IResult<IQueryable<DynamicContent>> GetContentByType(
            string contentType,
            ContentLifecycleStatus? status,
            Expression<Func<DynamicContent, bool>> filter = null);
    }
}

using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Blogs
{
    /// <summary>
    /// Represents the out of the box (OOB) Sitefinity OData blog service available
    /// </summary>
    public abstract class CoreBlogODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/blogs";

        public CoreBlogODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

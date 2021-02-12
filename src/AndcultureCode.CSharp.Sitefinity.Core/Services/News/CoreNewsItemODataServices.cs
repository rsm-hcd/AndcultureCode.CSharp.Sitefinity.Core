using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.News
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData news item service available
    /// </summary>
    public abstract class CoreNewsItemODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/newsitems";

        public CoreNewsItemODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

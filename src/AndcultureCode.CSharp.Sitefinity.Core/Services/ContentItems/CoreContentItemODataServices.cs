using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.ContentItems
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData content item service available
    /// </summary>
    public abstract class CoreContentItemODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/contentitems";

        public CoreContentItemODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Pages
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData page service available
    /// </summary>
    public abstract class CorePageODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/pages";

        public CorePageODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

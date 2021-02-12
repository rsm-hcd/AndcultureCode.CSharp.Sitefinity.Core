using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Images
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData image service available
    /// </summary>
    public abstract class CoreImageODataServices<TModel> : CoreMediaODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/images";

        public CoreImageODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

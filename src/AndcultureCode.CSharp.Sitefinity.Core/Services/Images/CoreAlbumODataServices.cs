using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Images
{
    /// <summary>
    /// Represents the out of the box (OOB) Sitefinity OData album service available
    /// </summary>
    public abstract class CoreAlbumODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/albums";

        public CoreAlbumODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

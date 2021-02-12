using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Documents
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData document library service available
    /// </summary>
    public abstract class CoreDocumentLibraryODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/documentlibraries";

        public CoreDocumentLibraryODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

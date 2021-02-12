using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Documents
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData document service available
    /// </summary>
    public abstract class CoreDocumentODataServices<TModel> : CoreMediaODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/documents";

        public CoreDocumentODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Calendars
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData event service available
    /// </summary>
    public abstract class CoreEventODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/events";

        public CoreEventODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

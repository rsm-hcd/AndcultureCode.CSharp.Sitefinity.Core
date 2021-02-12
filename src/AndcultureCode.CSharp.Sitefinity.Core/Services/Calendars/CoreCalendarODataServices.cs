using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services.Calendars
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData calendar service available
    /// </summary>
    public abstract class CoreCalendarODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public const string ENDPOINT_URL = "api/default/calendars";

        public CoreCalendarODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }
    }
}

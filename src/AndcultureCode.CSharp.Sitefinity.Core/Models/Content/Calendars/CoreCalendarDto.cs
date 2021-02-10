using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.Calendars
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoreCalendarDto : SitefinityContentDto
    {
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string Title { get; set; }
    }
}

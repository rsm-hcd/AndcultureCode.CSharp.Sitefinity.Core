using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.Calendars
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoreEventDto : ChildSitefinityContentDto
    {
        public bool AllDayEvent { get; set; }
        public bool AllowComments { get; set; }
        public List<Guid> Category { get; set; }
        public string City { get; set; }
        public List<IComment> Comments { get; set; }
        public string ContactCell { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactWeb { get; set; }
        public string Content { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? EventEnd { get; set; }
        public double? EventEndUtcOffset { get; set; }
        public DateTimeOffset? EventEndWithOffset { get; set; }
        public DateTimeOffset? EventStart { get; set; }
        public double? EventStartUtcOffset { get; set; }
        public DateTimeOffset? EventStartWithOffset { get; set; }
        public bool IsRecurrent { get; set; }
        public string Location { get; set; }
        public string RecurrentExpression { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Summary { get; set; }
        public List<Guid> Tags { get; set; }
        public string TimeZoneId { get; set; }
        public string Title { get; set; }
    }
}

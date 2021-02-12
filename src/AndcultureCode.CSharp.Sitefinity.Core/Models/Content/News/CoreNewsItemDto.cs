using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.News
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoreNewsItemDto : SitefinityContentDto
    {
        public bool AddToLatestNews { get; set; }
        public bool AllowComments { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public DateTime? RemoveFromLatestNewsBy { get; set; }
        public string SourceName { get; set; }
        public string SourceSite { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}

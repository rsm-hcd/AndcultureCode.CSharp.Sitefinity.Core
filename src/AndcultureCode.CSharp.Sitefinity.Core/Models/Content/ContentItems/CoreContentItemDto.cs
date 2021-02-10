using AndcultureCode.CSharp.Sitefinity.Core.Models.Content;
using Newtonsoft.Json;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.ContentItems
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoreContentItemDto : SitefinityContentDto
    {
        public string Content { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}

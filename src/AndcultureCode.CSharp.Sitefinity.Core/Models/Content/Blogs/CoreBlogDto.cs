using Newtonsoft.Json;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.Blogs
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoreBlogDto : SitefinityContentDto
    {
        public string Description { get; set; }
        public string Title { get; set; }
    }
}

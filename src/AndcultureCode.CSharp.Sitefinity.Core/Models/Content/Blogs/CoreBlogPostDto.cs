using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.Blogs
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoreBlogPostDto : ChildSitefinityContentDto
    {
        public bool AllowComments { get; set; }
        public List<Guid> Category { get; set; }
        public List<IComment> Comments { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public List<Guid> Tags { get; set; }
        public string Title { get; set; }
    }
}

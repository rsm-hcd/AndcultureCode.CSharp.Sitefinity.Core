using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SitefinityContentDto : ISitefinityContentDto
    {
        public DateTime? DateCreated { get; set; }
        public Guid? Id { get; set; }
        public DateTime? LastModified { get; }
        public DateTime? PublicationDate { get; set; }
        public string UrlName { get; set; }
    }
}

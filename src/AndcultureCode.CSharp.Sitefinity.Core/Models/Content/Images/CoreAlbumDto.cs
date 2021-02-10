using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.Images
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoreAlbumDto : SitefinityContentDto
    {
        public string BlobStorageProvider { get; set; }
        public int ChildrenCount { get; set; }
        public string ClientCacheProfile { get; set; }
        public Guid CoverId { get; set; }
        public string Description { get; set; }
        public long MaxItemSize { get; set; }
        public long MaxSize { get; set; }
        public string NewSize { get; set; }
        public string OutputCacheProfile { get; set; }
        public bool ResizeOnUpload { get; set; }
        public string Title { get; set; }
    }
}

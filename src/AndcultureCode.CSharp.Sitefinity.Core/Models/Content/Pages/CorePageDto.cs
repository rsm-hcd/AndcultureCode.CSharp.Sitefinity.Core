using AndcultureCode.CSharp.Sitefinity.Core.Enumerations;
using AndcultureCode.CSharp.Sitefinity.Core.JsonSerialization.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.Pages
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CorePageDto : SitefinityContentDto
    {
        public bool AllowParametersValidation { get; set; }
        public List<string> AvailableLanguages { get; set; }
        public List<string> Breadcrumb { get; set; }
        public object CanonicalUrlBehaviour { get; set; }
        public string CodeBehindType { get; set; }
        public bool Crawlable { get; set; }
        public string Description { get; set; }
        public bool EnableViewState { get; set; }
        public bool HasChildren { get; set; }
        public string HeadTagContent { get; set; }
        public string HtmlTitle { get; set; }
        public bool IncludeInSearchIndex { get; set; }
        public bool IncludeScriptManager { get; set; }
        public bool IsHomePage { get; set; }
        public object LocalizationStrategy { get; set; }
        public string OutputCacheProfile { get; set; }
        [JsonConverter(typeof(PageTypeConverter))]
        public PageType PageType { get; set; }
        public Guid? ParentId { get; set; }
        public double Priority { get; set; }
        [JsonConverter(typeof(RedirectPageConverter))]
        public RedirectPage? RedirectPage { get; set; }
        public string RelativeUrlPath { get; set; }
        public bool RequireSsl { get; set; }
        public Guid? RootId { get; set; }
        public bool ShowInNavigation { get; set; }
        public Guid TemplateId { get; set; }
        public string Title { get; set; }
        public string ViewUrl { get; set; }
    }
}

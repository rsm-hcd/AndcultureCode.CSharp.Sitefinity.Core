using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content
{
    public class SitefinityContent
    {
        public DateTime DateCreated { get; set; }
        public Guid Id { get; set; }
        public Guid OriginalContentId { get; set; }
        public ContentLifecycleStatus Status { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}

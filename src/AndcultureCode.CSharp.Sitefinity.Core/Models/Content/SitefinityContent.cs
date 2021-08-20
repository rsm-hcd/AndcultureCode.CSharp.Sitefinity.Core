using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content
{
    public class SitefinityContent
    {
        public DateTime DateCreated { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// Refers to the Master Content Item's ID. In the event that this object is the Master version, this value will
        /// be a GUID of all 0's.
        /// </summary>
        public Guid OriginalContentId { get; set; }
        public DateTime PublicationDate { get; set; }
        public ContentLifecycleStatus Status { get; set; }
        public Lstring UrlName { get; set; }
    }
}

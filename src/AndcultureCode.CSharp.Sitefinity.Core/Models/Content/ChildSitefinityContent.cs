using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content
{
    public class ChildSitefinityContent<T> : SitefinityContent
        where T : SitefinityContent
    {
        public Guid SystemParentId { get; set; }
        public T SystemParentItem { get; set; }
    }
}

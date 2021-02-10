using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content
{
    public class ChildSitefinityContentDto : SitefinityContentDto
    {
        public Guid? ParentId { get; set; }
    }
}

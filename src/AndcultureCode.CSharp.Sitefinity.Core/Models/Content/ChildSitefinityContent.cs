using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content
{
    public class ChildSitefinityContent<TParent> : SitefinityContent
        where TParent : SitefinityContent
    {
        public TParent SystemParentItem { get; set; }
    }
}

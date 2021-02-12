 using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Interfaces
{
    public interface ISitefinityContentDto
    {
        DateTime? DateCreated { get; set; }
        Guid? Id { get; set; }
        DateTime? PublicationDate { get; set; }
    }
}

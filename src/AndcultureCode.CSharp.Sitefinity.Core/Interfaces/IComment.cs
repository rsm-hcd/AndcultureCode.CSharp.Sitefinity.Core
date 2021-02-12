using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Interfaces
{
    public interface IComment
    {
        DateTimeOffset DateCreated { get; set; }
        string Key { get; set; }
        DateTimeOffset? LastModified { get; set; }
        string LastModifiedBy { get; set; }
        string Message { get; set; }
        string Status { get; set; }
    }
}

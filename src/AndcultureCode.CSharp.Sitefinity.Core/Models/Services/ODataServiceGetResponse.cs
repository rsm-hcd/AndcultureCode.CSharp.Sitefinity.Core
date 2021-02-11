using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using System.Collections.Generic;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Services
{
    public class ODataServiceGetResponse<T> where T : ISitefinityContentDto
    {
        public List<T> Value { get; set; }
    }
}

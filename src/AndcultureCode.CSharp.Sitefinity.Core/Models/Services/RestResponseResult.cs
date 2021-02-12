using AndcultureCode.CSharp.Core.Interfaces;
using AndcultureCode.CSharp.Core.Models.Errors;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Services
{
    public class RestResponseResult<T> : Result<T>
    {
        public HttpStatusCode ExpectedStatusCode { get; }

        /// <summary>
        /// Provides the actual rest response data in its original format
        /// </summary>
        public IRestResponse RestResponse { get; }

        /// <summary>
        /// Indicates whether the RestResponse's StatusCode was of the expected type after the request was made
        /// and its data value was returned
        /// </summary>
        public bool WasExpectedStatusCode => ExpectedStatusCode == RestResponse.StatusCode;
        public bool WasUnexpectedStatusCode => !WasExpectedStatusCode;

        public RestResponseResult(HttpStatusCode expectedStatusCode, IRestResponse restResponse) : base()
        {
            Errors = new List<IError>();
            ExpectedStatusCode = expectedStatusCode;
            RestResponse = restResponse;
        }
    }
}

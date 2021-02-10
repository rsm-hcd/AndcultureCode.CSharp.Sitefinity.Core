using AndcultureCode.CSharp.Core.Models.Errors;
using AndcultureCode.CSharp.Sitefinity.Core.Constants;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Services;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace AndcultureCode.CSharp.Sitefinity.Core.Extensions
{
    public static class RestResponseResultExtensions
    {
        /// <summary>
        /// Adds an INCORRECT_HTTP_RESPONSE_STATUS_CODE_RETURNED error key with appropriate 
        /// messaging to the result's error list
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="result"></param>
        /// <param name="expectedStatusCode"></param>
        /// <param name="actualStatusCode"></param>
        /// <param name="actionName">Such as "creating a draft"</param>
        /// <param name="requestBody">If body provided in request, pass that here</param>
        public static void AddUnexpectedStatusCodeError<TContent>(
            this RestResponseResult<TContent> result,
            HttpStatusCode expectedStatusCode,
            IRestResponse restResponse,
            string actionName,
            object requestBody = null
            )
        {
            var endOfError = "";
            if (requestBody != null)
            {
                endOfError += $" using request body of {JsonConvert.SerializeObject(requestBody)}";
            }
            if (restResponse.ErrorMessage != "")
            {
                endOfError += $" with IRestResponse.ErrorMessage of '{restResponse.ErrorMessage}'";
            }
            if (restResponse.Content != "")
            {
                endOfError += $" with IRestResponse.Content of '{restResponse.Content}'";
            }

            result.Errors.Add(new Error
            {
                Key = RequestErrorKeys.INCORRECT_HTTP_RESPONSE_STATUS_CODE_RETURNED,
                Message = $"The HTTP status code of '{expectedStatusCode}' was expected.  Instead, the HTTP status code of '{restResponse.StatusCode}' was returned when {actionName} for type '{typeof(TContent).FullName}'{endOfError}"
            });
        }
    }
}
using RestSharp;
using System.Net;

namespace AndcultureCode.CSharp.Sitefinity.Core.Extensions
{
    public static class RestResponseExtensions
    {
        public static bool WasNotAllowed(this IRestResponse restResponse)
        {
            var isCurrentUserNotAllowedAccess = restResponse.Content.Contains("The current user is not allowed access");
            var isUnauthorized = restResponse.StatusCode == HttpStatusCode.Unauthorized;
            return isUnauthorized && isCurrentUserNotAllowedAccess;
        }
    }
}
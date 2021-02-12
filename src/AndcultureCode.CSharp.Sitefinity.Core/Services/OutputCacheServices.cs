using RestSharp;
using System.IO;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services
{
    public class OutputCacheService
    {
        /// <summary>
        /// The Output Cache API requires its own authentication key.  This can be set using
        /// the `SitefinityODataTestSettings.OutputCacheAuthKey` setting within `appsettings.json`
        /// </summary>
        public string AuthKey { get; set; }
        public string BaseUrl { get; set; }

        public OutputCacheService(string baseUrl, string authKey)
        {
            AuthKey = authKey;
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Clears the output cache for use by the other oData services.  Use this when testing
        /// updates to the page services.  When POSTing, PUTing, and GETing all within a single
        /// test in that order, Sitefinity will intermittenly returned cached results in the GET
        /// from the original POST.  Make sure this is called before the GET is called to avoid
        /// caching issues.
        /// </summary>
        /// <returns>The response of the executed request.</returns>
        public IRestResponse Clear()
        {
            var client = new RestClient(Path.Combine(BaseUrl + "restapi/cache/clear"));

            var request = new RestRequest(Method.POST);

            request.AddHeader("SF_OUTPUTCACHE_AUTH", AuthKey);

            return client.Execute(request);
        }
    }
}

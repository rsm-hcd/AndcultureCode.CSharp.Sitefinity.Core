using AndcultureCode.CSharp.Core.Models.Errors;
using AndcultureCode.CSharp.Sitefinity.Core.Extensions;
using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.JsonSerialization;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Content;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services
{
    public abstract class ODataServices<TContent>
        where TContent : ISitefinityContentDto
    {

        #region Constants

        public const int SITEFINITY_MAX_RESULTS_ROW_COUNT = 50;

        #endregion Constants

        public abstract string EndpointUrl { get; }

        public IODataConnectionSettings Settings { get; set; }
        public ODataSession Session { get; set; }

        public ODataServices(IODataConnectionSettings settings, ODataSession session)
        {
            Session = session;
            Settings = settings;
        }

        #region Public Methods

        /// <summary>
        /// Creates a draft content item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is the returned object with its updated values</returns>
        public RestResponseResult<TContent> CreateDraft(TContent model)
        {
            var body = JsonConvert.SerializeObject(model);
            var requestUrl = Settings.BaseUrl + EndpointUrl;

            var restResponse = ExecuteSitefinityRequest(Method.POST, requestUrl, body);

            var result = new RestResponseResult<TContent>(HttpStatusCode.Created, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.Created, restResponse, "creating a draft", model);
                return result;
            }

            result.ResultObject = JsonConvert.DeserializeObject<TContent>(restResponse.Content);

            return result;
        }

        /// <summary>
        /// Create a relationship record between the main content and the related content
        /// </summary>
        /// <param name="id">The Id of the main content.</param>
        /// <param name="relatedId">The Id of the related content.</param>
        /// <param name="string">The name of the relationship.</param>
        /// <param name="string">The endpoint URL of the related item.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is whether the object was successfully created</returns>
        public RestResponseResult<bool> CreateRelated(Guid id, Guid relatedId, string relationshipName, string relationshipEndpointUrl)
        {
            var requestObject = new JObject(); ;
            var postODataId = $"{Settings.BaseUrl}{relationshipEndpointUrl}({relatedId})";
            requestObject.Add("@odata.id", new JValue(postODataId));
            var body = JsonConvert.SerializeObject(requestObject);
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({id})/{relationshipName}/$ref";

            var restResponse = ExecuteSitefinityRequest(Method.POST, requestUrl, body);

            var result = new RestResponseResult<bool>(HttpStatusCode.NoContent, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.NoContent, restResponse, "creating a relationship");

                result.ResultObject = false;
                return result;
            }

            result.ResultObject = true;
            return result;
        }

        /// <summary>
        /// Delete existing item.
        /// </summary>
        /// <param name="id">The Id of the content.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is whether the object was successfully deleted</returns>
        public RestResponseResult<bool> Delete(Guid id)
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({id})";

            var restResponse = ExecuteSitefinityRequest(Method.DELETE, requestUrl, null);

            var result = new RestResponseResult<bool>(HttpStatusCode.NoContent, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.NoContent, restResponse, "deleting data");

                result.ResultObject = false;
                return result;
            }

            result.ResultObject = true;
            return result;
        }

        /// <summary>
        /// Removes the related item based on the relationship name.
        /// </summary>
        /// <param name="id">The Id of the main content.</param>
        /// <param name="relatedId">The Id of the related content.</param>
        /// <param name="string">The name of the relationship.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is whether the related object was successfully deleted</returns>
        public RestResponseResult<bool> DeleteRelated(Guid id, Guid relatedId, string relationshipName)
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({id})/{relationshipName}({relatedId})/$ref";

            var restResponse = ExecuteSitefinityRequest(Method.DELETE, requestUrl, null);

            var result = new RestResponseResult<bool>(HttpStatusCode.NoContent, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.NoContent, restResponse, "deleting related data");

                result.ResultObject = false;
                return result;
            }

            result.ResultObject = true;
            return result;
        }

        /// <summary>
        /// Gets existing items based on the given parameters
        /// </summary>
        /// <param name="skip">The number of items to skip before returning the results.</param>
        /// <param name="take">The number of items to pull in the result.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is the returned object list</returns>
        public RestResponseResult<List<TContent>> Get(int? skip = null, int? take = null)
        {
            skip = skip ?? 0;
            take = take ?? SITEFINITY_MAX_RESULTS_ROW_COUNT;
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}?$skip={skip}&$take={take}";

            var restResponse = ExecuteSitefinityRequest(Method.GET, requestUrl, null);

            var result = new RestResponseResult<List<TContent>>(HttpStatusCode.OK, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.OK, restResponse, $"getting data items");
                return result;
            }

            result.ResultObject = JsonConvert.DeserializeObject<ODataServiceGetResponse<TContent>>(restResponse.Content).Value;

            return result;
        }

        /// <summary>
        /// Gets the number of the existing, published Sitefinity items for data type specified by this service
        /// </summary>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is the total number of published records</returns>
        public RestResponseResult<int> GetCount()
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}/$count";

            var restResponse = ExecuteSitefinityRequest(Method.GET, requestUrl, null);

            var result = new RestResponseResult<int>(HttpStatusCode.OK, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.OK, restResponse, "getting the total count of published");

                return result;
            }

            var content = restResponse.Content.Trim(new char[] { '\uFEFF' }); // Removing the leading Byte Order Mark (BOM) from content.

            result.ResultObject = Convert.ToInt32(content);

            return result;
        }

        /// <summary>
        /// Gets an existing item.
        /// </summary>
        /// <param name="id">The Id of the content.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is the returned object</returns>
        public RestResponseResult<TContent> GetItem(Guid id)
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({id})";

            var restResponse = ExecuteSitefinityRequest(Method.GET, requestUrl, null);

            var result = new RestResponseResult<TContent>(HttpStatusCode.OK, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.OK, restResponse, "getting item");

                return result;
            }

            result.ResultObject = JsonConvert.DeserializeObject<TContent>(restResponse.Content);

            return result;
        }

        /// <summary>
        /// Gets related items based on the relationship name.
        /// NOTE: Use this for multi-instance relationships
        /// </summary>
        /// <param name="id">The Id of the content.</param>
        /// <param name="string">The name of the relationship.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is the returned related object list</returns>
        public RestResponseResult<List<TRelatedContent>> GetRelatedRecords<TRelatedContent>(Guid id, string relationshipName) where TRelatedContent : SitefinityContentDto
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({id})/{relationshipName}";

            var restResponse = ExecuteSitefinityRequest(Method.GET, requestUrl, null);

            var result = new RestResponseResult<List<TRelatedContent>>(HttpStatusCode.OK, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.OK, restResponse, $"getting many related data items");
                return result;
            }

            result.ResultObject = JsonConvert.DeserializeObject<ODataServiceGetResponse<TRelatedContent>>(restResponse.Content).Value;

            return result;
        }

        /// <summary>
        /// Gets a related item based on the relationship name.
        /// NOTE: Use this for single instance relationships
        /// </summary>
        /// <param name="id">The Id of the content.</param>
        /// <param name="string">The name of the relationship.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is the returned related object</returns>
        public RestResponseResult<TRelatedContent> GetRelatedRecord<TRelatedContent>(Guid id, string relationshipName) where TRelatedContent : SitefinityContentDto
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({id})/{relationshipName}";

            var restResponse = ExecuteSitefinityRequest(Method.GET, requestUrl, null);

            var result = new RestResponseResult<TRelatedContent>(HttpStatusCode.OK, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.OK, restResponse, $"getting single related data item");

                return result;
            }

            result.ResultObject = JsonConvert.DeserializeObject<ODataServiceGetResponse<TRelatedContent>>(restResponse.Content).Value.FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Modify existing item.
        /// </summary>
        /// <param name="model">The content model.</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is the returned object with it's updated values</returns>
        public RestResponseResult<TContent> Modify(TContent model)
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({model.Id})";
            var body = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ContractResolver = new ShouldSerializeContractResolver() });
            var restResponse = ExecuteSitefinityRequest(Method.PATCH, requestUrl, body);

            var result = new RestResponseResult<TContent>(HttpStatusCode.NoContent, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.NoContent, restResponse, "modifying data");
                return result;
            }

            result.ResultObject = JsonConvert.DeserializeObject<TContent>(restResponse.Content);

            return result;
        }

        /// <summary>
        /// Publish an existing content item.
        /// </summary>
        /// <param name="id">The Id of the content</param>
        /// <returns>The response of the request wrapped in a RestResponseResult where ResultObject is whether the object was successfully published</returns>
        public RestResponseResult<bool> Publish(Guid id)
        {
            var requestUrl = $"{Settings.BaseUrl}{EndpointUrl}({id})/operation";
            var body = "{\"action\": \"Publish\",\"actionParameters\": {}}";

            var restResponse = ExecuteSitefinityRequest(Method.POST, requestUrl, body);

            var result = new RestResponseResult<bool>(HttpStatusCode.OK, restResponse);

            if (result.WasUnexpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.OK, restResponse, "publishing");

                result.ResultObject = false;
                return result;
            }

            result.ResultObject = true;
            return result;
        }

        /// <summary>
        /// Attempts to execute the Sitefinity OData request.  On failure due to an expired access token, this will attempt to
        /// re-authenticate and make the same request a second time before returning it's results
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public IRestResponse ExecuteAuthorizedRequest(RestClient client, RestRequest request)
        {
            var secondsTillExpiration = Session.AccessTokenExpiration.Subtract(DateTimeOffset.Now).TotalSeconds;

            if (secondsTillExpiration <= 30)
            {
                // session is going to expire in about 30 seconds... let's create a new session now before it's too late
                Session.Authenticate();
            }

            IRestResponse restResponse = client.Execute(request);

            if (restResponse.WasNotAllowed())
            {
                // the session still likely expired for whatever the reason... last ditch effort to salvage by creating another new session then re-executing OData request immediately after
                Session.Authenticate();
                return client.Execute(request);
            }

            return restResponse;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Executes rest request with the default headers required for Sitefinity.
        /// </summary>
        /// <param name="method">The Method of the request.</param>
        /// <param name="url">The url.</param>
        /// <param name="body">The body.</param>
        /// <returns>The response of the executed request.</returns>
        private IRestResponse ExecuteSitefinityRequest(Method method, string url, object body)
        {
            var client = new RestClient(url);

            var request = new RestRequest(method);

            request.AddHeader("authorization", "Bearer " + Session.AccessToken);
            request.AddHeader("x-sf-service-request", "true");
            request.AddHeader("content-type", "application/json");

            if (body != null)
            {
                request.AddParameter("application/json", body.ToString(), ParameterType.RequestBody);
            }

            IRestResponse response = ExecuteAuthorizedRequest(client, request);

            return response;
        }

        #endregion Private Methods
    }
}

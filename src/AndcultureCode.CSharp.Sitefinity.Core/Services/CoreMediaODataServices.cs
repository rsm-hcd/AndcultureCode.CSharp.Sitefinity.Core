using AndcultureCode.CSharp.Sitefinity.Core.Extensions;
using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;
using System.Net;
using System.Web;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services
{
    /// <summary>
    /// Represents the out of the box  Sitefinity OData media service available
    /// </summary>
    public abstract class CoreMediaODataServices<TModel> : ODataServices<TModel>
        where TModel : ISitefinityContentDto
    {
        public CoreMediaODataServices(IODataConnectionSettings settings, ODataSession session) : base(settings, session) { }

        /// <summary>
        /// Creates the new model and uploads the media asset using the filePath asset provided
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public virtual RestResponseResult<TModel> UploadAndCreate(TModel model, string filePath)
        {
            var fileData = File.ReadAllBytes(filePath);
            var fileInfo = new FileInfo(filePath);
            var mimeType = MimeMapping.GetMimeMapping(filePath);

            var jObject = JObject.FromObject(model);
            jObject.Add("DirectUpload", true);

            var url = $"{Settings.BaseUrl}{EndpointUrl}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "Bearer " + Session.AccessToken);
            request.AddHeader("X-File-Name", fileInfo.Name);
            request.AddHeader("X-Sf-Properties", jObject.ToString(Formatting.None));
            request.AddParameter(mimeType, fileData, ParameterType.RequestBody);

            IRestResponse response = ExecuteAuthorizedRequest(client, request);

            var result = new RestResponseResult<TModel>(HttpStatusCode.Created, response);
            if (!result.WasExpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.Created, response, $"uploading {typeof(TModel).FullName}", model);
                return result;
            }

            result.ResultObject = JsonConvert.DeserializeObject<TModel>(response.Content);

            return result;
        }

        /// <summary>
        /// Updates the existing model and uploads and overwrites the current media asset using the filePath asset provided
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public virtual RestResponseResult<bool> UploadAndOverwrite(TModel model, string filePath)
        {
            var fileData = File.ReadAllBytes(filePath);
            var fileInfo = new FileInfo(filePath);
            var mimeType = MimeMapping.GetMimeMapping(filePath);

            var jObject = JObject.FromObject(model);
            jObject.Add("DirectUpload", true);

            var url = $"{Settings.BaseUrl}{EndpointUrl}/{model.Id}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("authorization", "Bearer " + Session.AccessToken);
            request.AddHeader("X-File-Name", fileInfo.Name);
            request.AddHeader("X-Sf-Properties", jObject.ToString(Formatting.None));
            request.AddParameter(mimeType, fileData, ParameterType.RequestBody);

            IRestResponse response = ExecuteAuthorizedRequest(client, request);

            var result = new RestResponseResult<bool>(HttpStatusCode.NoContent, response);
            if (!result.WasExpectedStatusCode)
            {
                result.AddUnexpectedStatusCodeError(HttpStatusCode.NoContent, response, $"uploading {typeof(TModel).FullName}", model);
                result.ResultObject = false;
                return result;
            }

            result.ResultObject = true;
            return result;
        }
    }
}

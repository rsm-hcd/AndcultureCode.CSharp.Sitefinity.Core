using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Core.Models.Services;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Services
{
    public class ODataSession
    {
        #region Member Variables

        private AuthenticateResponse _authenticateResponse;
        private DateTimeOffset _accessTokenExpiration;

        public string AccessToken { get => _authenticateResponse?.AccessToken; }
        public DateTimeOffset AccessTokenExpiration { get => _accessTokenExpiration; }
        public bool HasAccessTokenExpired { get => _accessTokenExpiration.CompareTo(DateTimeOffset.Now) <= 0; }

        public IODataConnectionSettings ODataConnectionSettings { get; }

        private string AuthenticateUrl => ODataConnectionSettings.BaseUrl + "/Sitefinity/Authenticate/OpenID/connect/token";

        #endregion Member Variables

        #region Constructor

        public ODataSession (IODataConnectionSettings oDataConnectionSettings)
        {
            ODataConnectionSettings = oDataConnectionSettings;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Attempts to create a session with the OData service layer.  This will throw an exception if unable to start a session.
        /// </summary>
        public void Authenticate()
        {
            var settings = ODataConnectionSettings;
            var client = new RestClient(AuthenticateUrl);
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            string authHeaderContent = string.Format("username={0}&password={1}&grant_type={2}&scope={3}&client_id={4}&client_secret={5}",
                settings.Username, settings.Password, settings.GrantType, settings.Scope, settings.ClientID, settings.ClientSecret);

            // Make sure you have add this client to the authentication config.
            request.AddParameter("auth", authHeaderContent, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if ((int)response.StatusCode != 200)
            {
                var settingsAsString = JsonConvert.SerializeObject(settings);
                var content = $"response.Content: { response.Content}";
                var errorMessage = $"response.ErrorMessage: { response.ErrorMessage}";
                throw new Exception($"Unable to authenticate to URL: '{AuthenticateUrl}' using settings '{settingsAsString}' --- {content} --- {errorMessage} ");
            }
            _authenticateResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content);
            _accessTokenExpiration = DateTimeOffset.Now.AddSeconds(_authenticateResponse.ExpiresIn);
        }

        #endregion Public Methods
    }
}

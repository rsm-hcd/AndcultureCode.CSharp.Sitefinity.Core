using Newtonsoft.Json;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Services
{
    public class AuthenticateResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// The number of seconds until Sitefinity will expire the session.
        /// Defaults to 3,600 seconds (1 hour)
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}

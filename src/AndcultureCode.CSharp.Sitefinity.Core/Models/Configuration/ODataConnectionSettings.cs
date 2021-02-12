using AndcultureCode.CSharp.Sitefinity.Core.Interfaces;
using AndcultureCode.CSharp.Sitefinity.Core.Utilities;
using System;
using System.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration
{
    public class ODataConnectionSettings : IODataConnectionSettings
    {
        #region Public Members

        public string BaseUrl { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
        public string OutputCacheAuthKey { get; set; }
        public string Password { get; set; }
        public string Scope { get; set; }
        public string Username { get; set; }

        #endregion Public Members

        public ODataConnectionSettings()
        {
            // Set defaults here
            BaseUrl = "";
            ClientID = "";
            ClientSecret = "";
            GrantType = "";
            OutputCacheAuthKey = "";
            Password = "";
            Scope = "";
            Username = "";

            var baseUrlValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__BASEURL");
            if (baseUrlValue != null)
            {
                BaseUrl = baseUrlValue;
            }
            var clientIDValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__CLIENTID");
            if (clientIDValue != null)
            {
                ClientID = clientIDValue;
            }
            var clientSecretValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__CLIENTSECRET");
            if (clientSecretValue != null)
            {
                ClientSecret = clientSecretValue;
            }
            var grantTypeValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__GRANTTYPE");
            if (grantTypeValue != null)
            {
                GrantType = grantTypeValue;
            }
            var outputCacheAuthKeyValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__OUTPUTCACHEAUTHKEY");
            if (outputCacheAuthKeyValue != null)
            {
                OutputCacheAuthKey = outputCacheAuthKeyValue;
            }
            var passwordValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__PASSWORD");
            if (passwordValue != null)
            {
                Password = passwordValue;
            }
            var scopeValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__SCOPE");
            if (scopeValue != null)
            {
                Scope = scopeValue;
            }
            var usernameValue = EnvironmentUtils.GetValueFromEnvironmentVariableOrAppSettings("SITEFINITYODATATESTSETTINGS__USERNAME");
            if (usernameValue != null)
            {
                Username = usernameValue;
            }
        }
    }
}

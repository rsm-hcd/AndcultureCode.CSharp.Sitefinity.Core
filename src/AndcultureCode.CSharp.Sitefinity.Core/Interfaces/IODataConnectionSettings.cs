namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration
{
    public interface IODataConnectionSettings
    {
        string BaseUrl { get; set; }
        string ClientID { get; set; }
        string ClientSecret { get; set; }
        string GrantType { get; set; }
        string OutputCacheAuthKey { get; set; }
        string Password { get; set; }
        string Scope { get; set; }
        string Username { get; set; }
    }
}
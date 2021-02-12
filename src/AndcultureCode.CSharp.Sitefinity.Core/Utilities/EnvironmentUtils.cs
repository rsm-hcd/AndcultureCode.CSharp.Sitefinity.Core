using System;
using System.Configuration;

namespace AndcultureCode.CSharp.Sitefinity.Core.Utilities
{
    public static class EnvironmentUtils
    {
        public static string GetValueFromEnvironmentVariableOrAppSettings(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? ConfigurationManager.AppSettings[key];
        }
    }
}
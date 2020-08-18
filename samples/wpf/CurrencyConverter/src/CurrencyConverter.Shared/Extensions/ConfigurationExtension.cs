using System;
using Microsoft.Extensions.Configuration;

namespace CurrencyConverter.Shared.Extensions
{
    /// <summary>
    /// Extension class for IConfiguration
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// return the key if present in configuration file
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties</param>
        /// <param name="key">key to be read</param>
        /// <returns>value</returns>
        public static string GetKey(this IConfiguration configuration, string key)
        {
            if (string.IsNullOrWhiteSpace(configuration[key]))
            {
                throw new ArgumentException(String.Format($"It is missing the argument {key}"));
            }

            return configuration[key];
        }

        /// <summary>
        /// return the key as int if present in configuration file
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties</param>
        /// <param name="key">key to be read</param>
        /// <returns>value</returns>
        public static int GetKeyAsInt(this IConfiguration configuration, string key)
        {
            if (string.IsNullOrWhiteSpace(configuration[key]))
            {
                throw new ArgumentException(String.Format($"It is missing the argument {key}"));
            }
            if (Int32.TryParse(configuration[key], out int result))
            {
                return result;
            }
            throw new Exception($"Error converting to int. Key: {key}");
        }
    }
}

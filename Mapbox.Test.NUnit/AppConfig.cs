using System;
using System.Collections.Generic;
using System.Text;

namespace Mapbox.Test.NUnit
{
    using Microsoft.Extensions.Configuration;

    public static class AppConfig
    {
        private const string MapboxApiKeyEnvironmentVar = "MAPBOX_ACCESS_TOKEN";

        private static IConfigurationRoot _config;

        private static string _mapboxApiKey;

        static AppConfig()
        {
            _mapboxApiKey = Environment.GetEnvironmentVariable(MapboxApiKeyEnvironmentVar);
            if (!string.IsNullOrWhiteSpace(_mapboxApiKey))
            {
                Console.WriteLine($"Using {MapboxApiKeyEnvironmentVar}={_mapboxApiKey}");
            }

            _config = new ConfigurationBuilder().AddJsonFile("appSettings.json", false).Build();
            _mapboxApiKey = _config["Mapbox:ApiKey"];
            if (string.IsNullOrWhiteSpace(_mapboxApiKey))
            {
                throw new KeyNotFoundException("Mapbox API Key not found in appSettings.json");
            }

            Console.WriteLine($"Using {MapboxApiKeyEnvironmentVar}={_mapboxApiKey}");
            Environment.SetEnvironmentVariable(MapboxApiKeyEnvironmentVar, _mapboxApiKey);
        }

        public static string MapboxApiKey => _mapboxApiKey;

        public static string StringValue(string var) => _config[var];

        public static bool BoolValue(string var) => bool.Parse(_config[var]);

        public static int IntValue(string var) => int.Parse(_config[var]);

        public static double DoubleValue(string var) => double.Parse(_config[var]);
    }
}

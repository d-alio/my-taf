using Microsoft.Extensions.Configuration;

namespace CoreLayer
{
    // This class serves as a centralized configuration manager for the solution,
    // loading settings from an appsettings.json file and providing them through static properties.
    public class Configuration
    {
        // These store configuration values.
        // They're static (shared across the entire slution)
        // with private set (only this class can modify them).
        public static string BrowserType { get; private set; }

        public static string AppUrl { get; private set; }

        public static string TestDataPath { get; private set; }

        //The => symbol is called the expression-bodied member syntax(or lambda arrow).
        //In this context, it's a shorthand way to define a method or constructor body.
        static Configuration() => Init();

        //This creates a configuration pipeline that:
        //Looks in the current directory
        //Reads from appsettings.json
        //Falls back to defaults if values are missing(optional: true)
        public static void Init()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Uses the null-coalescing operator (??) to check if a value is null and provide an alternative value
            // if the config file is missing or doesn't contain these keys.
            BrowserType = configuration["BrowserType"] ?? "Chrome";
            AppUrl = configuration["ApplicationUrl"] ?? string.Empty;
            TestDataPath = configuration["TestDataPath"] ?? string.Empty;
        }
    }
}
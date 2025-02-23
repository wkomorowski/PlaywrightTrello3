using Microsoft.Extensions.Configuration;

namespace PlaywrightTrello2nd.Utilities;

public static class ConfigLoader
{
    private static readonly IConfiguration _configuration;
    private static readonly string _configFile = "appsettings.json";

    
    //public string ConfigFile => _configFile;
    static ConfigLoader()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_configFile);
        _configuration = builder.Build();
    }
    public static T GetSection<T>(string sectionName) where T: new()
    {
        var section = new T();
        _configuration.GetSection(sectionName).Bind(section);
        return section;
    }
}
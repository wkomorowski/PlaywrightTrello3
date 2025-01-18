using Microsoft.Extensions.Configuration;

namespace PlaywrightTrello2nd.Utilities;

public class ConfigLoader
{
    public IConfiguration Configuration { get; }
    
    public ConfigLoader()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        Configuration = builder.Build();
    }
    public T GetSection<T>(string sectionName) where T: new()
    {
        var section = new T();
        Configuration.GetSection(sectionName).Bind(section);
        return section;
    }
}
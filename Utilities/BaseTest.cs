using Microsoft.Playwright.NUnit;

namespace PlaywrightTrello2nd.Utilities;

public class BaseTest: PageTest
{
    public string BaseUrl { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public BaseTest()
    {
        BaseUrl = Environment.GetEnvironmentVariable("WEB_URL") ?? "https://test8765.com";
        UserName = Environment.GetEnvironmentVariable("WEB_USERNAME") ?? "admin";
        Password = Environment.GetEnvironmentVariable("WEB_PASSWORD") ?? "123456";
    }
}
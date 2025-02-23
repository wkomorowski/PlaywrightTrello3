using Microsoft.Playwright;

namespace PlaywrightTrello2nd.Pages;

public class PlaywrightTestBase
{
    public IBrowser Browser { get; set; }

    [SetUp]
    public async Task SetUp()
    {
        var playwright = await Playwright.CreateAsync();
        Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
    }
}
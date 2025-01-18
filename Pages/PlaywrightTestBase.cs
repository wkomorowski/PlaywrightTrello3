using Microsoft.Playwright;

namespace PlaywrightTrello2nd.Pages;

public class PlaywrightTestBase
{
    public IBrowser Browser { get; set; }
    protected IPage Page { get; set; }

    [SetUp]
    public async Task SetUp()
    {
        var playwright = await Playwright.CreateAsync();
        Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var context = await Browser.NewContextAsync();
        Page = await context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await Page.CloseAsync();
        await Browser.CloseAsync();
    }
}
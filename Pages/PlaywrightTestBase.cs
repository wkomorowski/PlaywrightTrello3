using Microsoft.Playwright;

namespace PlaywrightTrello2nd.Pages;

public class PlaywrightTestBase
{
    protected IBrowser Browser;
    protected IPlaywright Playwright;
    protected IBrowserContext Context;
    protected IPage Page;

    [OneTimeSetUp]
    public async Task BeforAllTests()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
    }
    [SetUp]
    public async Task BeforeEachTests()
    {
        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task AfterEachTests()
    {
        await Context.DisposeAsync();
    }

    public async Task AfterAllTests()
    {
        await Browser.CloseAsync();
        Playwright.Dispose();
    }
}
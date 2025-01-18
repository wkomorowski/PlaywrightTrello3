using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTrello2nd.Pages;
using PlaywrightTrello2nd.Utilities;

namespace PlaywrightTrello2nd;

public class Tests: PlaywrightTestBase
{
    [Test]
    public async Task LoginTest()
    {
        await Page.GotoAsync("https://www.trello.com");
        var configLoader = new ConfigLoader();
        var credentials = configLoader.GetSection<Credentials>("credentials");
        
        var page = new LoginPage(Page);
        await page.ClickLoginLnk();
        await page.Login(credentials.Username, credentials.Password);
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "screen57.jpg",
        });
        await page.ClickCreate();

        Assert.That(await page.IsMemberLinkExists(), Is.True);
    }
}

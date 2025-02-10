using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTrello2nd.Pages;
using PlaywrightTrello2nd.Utilities;

namespace PlaywrightTrello2nd;

public class LoginTest: PlaywrightTestBase
{
    [Test]
    public async Task Login()
    {
        //Prepare credentials
        var configLoader = new ConfigLoader();
        var credentials = configLoader.GetSection<Credentials>("credentials");
        
        await Page.GotoAsync("https://www.trello.com");
        
        var page = new LoginPage(Page);
        await page.ClickLoginLnk();
        await page.Login(credentials.Username, credentials.Password);
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = $"screen{Generator.RandomString(3)}.jpg"
        });
        await page.ClickCreate();

        Assert.That(await page.IsMemberLinkExists(), Is.True);
    }
}

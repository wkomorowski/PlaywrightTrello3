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
        var credentials = ConfigLoader.GetSection<Credentials>("credentials");
        
        await Page.GotoAsync(credentials.Website);
        
        var loginPage = new LoginPage(Page);
        await loginPage.ClickLoginLnk();
        await loginPage.Login(credentials.Username, credentials.Password);
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = $"screen{Generator.RandomString(3)}.jpg"
        });
        await loginPage.CreateBtn.ClickAsync();

        Assert.That(await loginPage.IsMemberLinkExists(), Is.True);
    }
}

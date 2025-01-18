using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTrello2nd.Pages;
using PlaywrightTrello2nd.Utilities;

namespace PlaywrightTrello2nd;

public class Tests: BaseTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.GetByTestId("bignav").GetByTestId("logo_link").IsVisibleAsync();
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "trello26.jpg",
        });
    }

    [Test]
    public async Task LoginTest()
    {
        
        string userName = UserName;
        string password = Password;
        
        var page = new LoginPage(Page);
        await page.ClickLoginLnk();
        await page.Login(userName, password);
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "trello27.jpg",
        });
        await page.ClickCreate();
        
        Assert.That(await page.IsMemberLinkExists(), Is.True.ToString());
    }
}

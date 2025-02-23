using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTrello2nd.Pages;
using PlaywrightTrello2nd.Utilities;

namespace PlaywrightTrello2nd;

public class LoginTest: PlaywrightTestBase
{
    private IPage Page { get; set; }
    [SetUp]
    public async Task BeforeEachTest()
    {
        var context = await Browser.NewContextAsync();
        Page = await context.NewPageAsync();
    }
    [TearDown]
    public async Task TearDown()
    {
        await Page.CloseAsync();
        await Browser.CloseAsync();
    }
    [Test]
    public async Task Login()
    {
        //Prepare credentials
        var credentials = ConfigLoader.GetSection<Credentials>("credentials");
        
        await Page.GotoAsync(credentials.Website);
        
        var page = new LoginPage(Page);
        await page.ClickLoginLnk();
        await page.Login(credentials.Username, credentials.Password);
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = $"screen{Generator.RandomString(3)}.jpg"
        });
        await page.ClickCreateBoardLink(); //Metoda klika button Create board.

        Assert.That(await page.IsMemberLinkExists(), Is.True);
    }
}

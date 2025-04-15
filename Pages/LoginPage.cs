using Microsoft.Playwright;

namespace PlaywrightTrello2nd.Pages;

public class LoginPage
{
    private IPage _page;
    public LoginPage(IPage page) => _page = page;
    private ILocator loginLink => _page.GetByTestId("bignav").Locator("text=Log in");
    private ILocator usernameTxt => _page.Locator("#username");
    private ILocator passwordTxt => _page.Locator("#password");
    private ILocator loginBtn => _page.Locator("#login-submit");
    public ILocator CreateBtn => _page.GetByTestId("header-create-menu-button");
    private ILocator memberLink => _page.GetByTestId("header-member-menu-avatar");
    
    public async Task ClickLoginLnk() => await loginLink.ClickAsync();
    public async Task Login(string userName, string password)
    {
        await usernameTxt.FillAsync(userName);
        await loginBtn.ClickAsync();
        await passwordTxt.FillAsync(password);
        await loginBtn.ClickAsync();
    }
    public async Task<bool> IsMemberLinkExists() => await memberLink.IsVisibleAsync();
}
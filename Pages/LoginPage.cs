using Microsoft.Playwright;

namespace PlaywrightTrello2nd.Pages;

public class LoginPage
{
    private IPage _page;
    public LoginPage(IPage page) => _page = page;
    private ILocator LoginLink => _page.GetByTestId("bignav").Locator("text=Log in");
    private ILocator UsernameTxt => _page.Locator("#username");
    private ILocator PasswordTxt => _page.Locator("#password");
    private ILocator LoginBtn => _page.Locator("#login-submit");
    private ILocator CreateBtn => _page.GetByTestId("header-create-menu-button");
    private ILocator MemberLink => _page.GetByTestId("header-member-menu-avatar");
    
    public async Task ClickLoginLnk() => await LoginLink.ClickAsync();
    public async Task Login(string userName, string password)
    {
        await UsernameTxt.FillAsync(userName);
        await LoginBtn.ClickAsync();
        await PasswordTxt.FillAsync(password);
        await LoginBtn.ClickAsync();
    }
    public async Task ClickCreate() => await CreateBtn.ClickAsync();
    public async Task<bool> IsMemberLinkExists() => await MemberLink.IsVisibleAsync();
}
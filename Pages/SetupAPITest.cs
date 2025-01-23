using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTrello2nd.Pages;

public class SetupAPITest : PlaywrightTest
{
    protected IAPIRequestContext Request { get; set; }

    protected string BaseUrl { get; } = "https://api.trello.com/1/";
}
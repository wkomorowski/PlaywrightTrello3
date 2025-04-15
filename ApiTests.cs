using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTrello2nd.Pages;
using PlaywrightTrello2nd.Utilities;

namespace PlaywrightTrello2nd;

public class ApiTests : PlaywrightTest
{
    private TrelloService trelloService;
    [SetUp]
    public async Task BeforeEachTest()
    {
        var apiRequest = await Playwright.APIRequest.NewContextAsync();
        var credentials = ConfigLoader.GetSection<CredentialsAPI>("CredentialsAPI");
        trelloService = new TrelloService(apiRequest, credentials.BaseUrl, credentials.ApiKey, credentials.Token);
    }

    [TearDown]
    public async Task AfterEachTest()
    {
        switch (trelloService)
        {
            case IDisposable disposable:
                disposable.Dispose();
                break;
            case IAsyncDisposable asyncDisposable:
                await asyncDisposable.DisposeAsync();
                break;
        }
    }
    
    [Test]
    public async Task ApiAuthTest_1()
    {
        var response = trelloService.LoginAuth();
        Assert.Multiple(() =>
        {
            Assert.That(response.Result.StatusText, Is.EqualTo("OK"));
            Assert.That(response.Result.Status.ToString(), Is.EqualTo("200"));
        });
        Console.WriteLine($"Status code: {response.Result.Status}.");
        Console.WriteLine($"Status test: {response.Result.StatusText}."); 
    }
    [Test]
    public async Task ApiBoardTest_2()
    {
        var credentials = ConfigLoader.GetSection<CredentialsAPI>("CredentialsAPI");
        
        var name = Generator.RandomString(5);
        
        var response = trelloService.CreateBoard(name);
        Assert.That(response.Result.Status.ToString(), Is.EqualTo("200"));
        
        var responseData = await response.Result.JsonAsync();

        var boardName = responseData.Value.GetProperty("name").ToString();
        var boardId = responseData.Value.GetProperty("id");
        
        Assert.That(boardName, Is.EqualTo(name));
        Console.WriteLine($"Created board name \"{boardName}\" with id {boardId}.");
        
        var deleteBoard = trelloService.Board("delete", boardId.ToString());
        Assert.That(deleteBoard.Result.Status.ToString(), Is.EqualTo("200"));
        
        var deletedBoard = trelloService.Board("get", boardId.ToString());
        Assert.That(deletedBoard.Result.StatusText, Is.EqualTo("Not Found"));
    }
}
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Playwright;
using PlaywrightTrello2nd.Pages;
using PlaywrightTrello2nd.Utilities;

namespace PlaywrightTrello2nd;

public class ApiTests : SetupAPITest
{
    private IAPIRequestContext Request;
    private TrelloService TrelloService;
    [SetUp]
    public async Task BeforeEachTest()
    {
        Request = await Playwright.APIRequest.NewContextAsync();
        var credentials = ConfigLoader.GetSection<CredentialsAPI>("CredentialsAPI");
        TrelloService = new TrelloService(Request, credentials.BaseUrl, credentials.ApiKey, credentials.Token);
    }
    
    [Test]
    public async Task ApiAuthTest_1()
    {
        var response = TrelloService.LoginAuth();
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
        
        //Creating a new board
        var response = TrelloService.CreateBoard(name);
        Assert.That(response.Result.Status.ToString(), Is.EqualTo("200"));
        
        var responseData = await response.Result.JsonAsync();

        var boardName = responseData.Value.GetProperty("name").ToString();
        var boardId = responseData.Value.GetProperty("id");
        
        Assert.That(boardName, Is.EqualTo(name));
        Console.WriteLine($"Created board name \"{boardName}\" with id {boardId}.");
        
        //Deleting created board
        var deleteBoard = TrelloService.Board("delete", boardId.ToString());
        Assert.That(deleteBoard.Result.Status.ToString(), Is.EqualTo("200"));
        
        //Check if deleted board is really not there anymore
        var deletedBoard = TrelloService.Board("get", boardId.ToString());
        Assert.That(deletedBoard.Result.StatusText, Is.EqualTo("Not Found"));
    }
}
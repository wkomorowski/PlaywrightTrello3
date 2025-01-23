using System.Diagnostics;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTrello2nd.Pages;
using PlaywrightTrello2nd.Utilities;

namespace PlaywrightTrello2nd;

public class ApiTests : SetupAPITest
{
    
    [Test]
    public async Task ApiAuthTest_1()
    {
        var configLoader = new ConfigLoader();
        var credentials = configLoader.GetSection<CredentialsAPI>("CredentialsAPI");

        Request = await Playwright.APIRequest.NewContextAsync();
        var response =
            await Request.GetAsync(
                $"{BaseUrl}member/me?key={credentials.ApiKey}&token={credentials.Token}");
        var responseData = await response.JsonAsync();
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusText.ToString(), Is.EqualTo("OK"));
            Assert.That(response.Status.ToString(), Is.EqualTo("200"));
        });
        //Console.WriteLine(responseData);
        Console.WriteLine($"Status code: {response.Status}.");
        Console.WriteLine($"Status test: {response.StatusText}."); 
    }

    [Test]
    public async Task ApiBoardTest_2()
    {
        var configLoader = new ConfigLoader();
        var credentials = configLoader.GetSection<CredentialsAPI>("CredentialsAPI");
        
        var name = Generator.RandomString(5);
        
        Request = await Playwright.APIRequest.NewContextAsync();
        var response = await Request.PostAsync($"{BaseUrl}boards/?name={name}&key={credentials.ApiKey}&token={credentials.Token}");
        Assert.That(response.Status.ToString(), Is.EqualTo("200"));
        
        var responseData = await response.JsonAsync();

        var boardName = responseData.Value.GetProperty("name").ToString();
        var boardId = responseData.Value.GetProperty("id");
        
        Assert.That(boardName, Is.EqualTo(name));
        Console.WriteLine($"Created board name {boardName} with id {boardId}.");
        
        var deleteBoard = await Request.DeleteAsync($"{BaseUrl}boards/{boardId}?key={credentials.ApiKey}&token={credentials.Token}");
        Assert.That(deleteBoard.Status.ToString(), Is.EqualTo("200"));
        
        //Check if deleted board is really not there anymore
        var deletedBoard = await Request.GetAsync($"{BaseUrl}boards/{boardId}?key={credentials.ApiKey}&token={credentials.Token}");
        Assert.That(deletedBoard.StatusText, Is.EqualTo("Not Found"));
    }
}
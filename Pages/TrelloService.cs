using Microsoft.Playwright;
using PlaywrightTrello2nd.Utilities;
using System.Text.Json.Nodes;

namespace PlaywrightTrello2nd.Pages;

public class TrelloService
{
    private readonly IAPIRequestContext _request;
    private readonly string _baseUrl;
    private readonly string _apiKey;
    private readonly string _token;
    
    public TrelloService(IAPIRequestContext request, string baseUrl, string apiKey, string token)
    {
        _request = request;
        _baseUrl = baseUrl;
        _apiKey = apiKey;
        _token = token;
    }

    public async Task<IAPIResponse> LoginAuth() => await _request.GetAsync($"{_baseUrl}member/me?key={_apiKey}&token={_token}");
    public async Task<IAPIResponse> CreateBoard(string boardName) => await _request.PostAsync($"{_baseUrl}boards/?name={boardName}&key={_apiKey}&token={_token}");
    public async Task<IAPIResponse> Board(string method, string boardId) =>
        method switch
        {
            "get" => await _request.GetAsync($"{_baseUrl}boards/{boardId}?key={_apiKey}&token={_token}"),
            "delete" => await _request.DeleteAsync($"{_baseUrl}boards/{boardId}?key={_apiKey}&token={_token}"),
            _ => throw new ArgumentException($"Unsupported HTTP method ({method}). You can only use 'get' or 'delete'.", nameof(method))
        };

}
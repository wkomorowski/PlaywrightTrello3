namespace PlaywrightTrello2nd.Utilities;

public class CredentialsAPI
{
    private string _apikey;
    private string _token;
    private string _baseUrl;
    public string ApiKey
    {
        get { return _apikey; }
        set { _apikey = value; }
    }
    public string Token
    {
        get { return _token; }
        set { _token = value; }
    }
    public string BaseUrl
    {
        get { return _baseUrl; }
        set { _baseUrl = value; }
    }
}
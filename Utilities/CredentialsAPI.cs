namespace PlaywrightTrello2nd.Utilities;

public class CredentialsAPI
{
    private string _apikey;
    private string _token;

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
}
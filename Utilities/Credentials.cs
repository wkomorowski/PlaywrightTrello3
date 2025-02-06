namespace PlaywrightTrello2nd.Utilities;

public class Credentials
{
    private string _username;
    private string _password;
    private string _website;

    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }
    public string Website
    {
        get { return _website; }
        set { _website = value; }
    }
}
namespace PlaywrightTrello2nd.Utilities;

public static class Generator
{
    public static string RandomString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPRSTUWVXYZ1234567890abcdefghijklmnoprstuwvxyz!@#$%^&*()";
        var stringChars = new char[length];
        var random = new Random();
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = characters[random.Next(characters.Length)];
        }
        var finalString = new string(stringChars);
        return finalString;
    }
}
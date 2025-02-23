using Bogus;
using Bogus.Extensions;

namespace PlaywrightTrello2nd.Utilities;

public class Generator
{
    public static string RandomString(int length)
    {
        var faker = new Faker();
        return faker.Random.String2(length);
    }
    
}
using System.Collections;

namespace SmsSendingApp.IntegrationTests.SeedData;

public class PostSmsTestValidDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { (int)Constants.CountryCodes.Cyprus, string.Concat(Enumerable.Repeat("a", 150)), 1 },
        new object[] { (int)Constants.CountryCodes.Cyprus, string.Concat(Enumerable.Repeat("a", 160)), 1 },
        new object[] { (int)Constants.CountryCodes.Cyprus, string.Concat(Enumerable.Repeat("a", 180)), 2 },
        new object[] { (int)Constants.CountryCodes.Cyprus, string.Concat(Enumerable.Repeat("a", 400)), 3 },
        new object[] { (int)Constants.CountryCodes.Greece, string.Concat(Enumerable.Repeat("α", 150)), 1 },
        new object[] { (int)Constants.CountryCodes.Greece, string.Concat(Enumerable.Repeat("α", 200)), 1 },
        new object[] { 44, string.Concat(Enumerable.Repeat("a", 150)), 1 },
        new object[] { 359, string.Concat(Enumerable.Repeat("a", 200)), 1 },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
using Airport.Measure.Implementation.Repositories.Web.Http;

namespace Airport.Measure.Implementation.Tests.Http;

[TestFixture]
public class HttpGetServiceTests
{
    [Test]
    [Ignore("Make real request. Should be in integration tests")]
    public async Task ShouldGetResponseTest()
    {
        // arrange 
        var baseUrl = "https://places-dev.cteleport.com/airports";

        var http = new HttpGetService(baseUrl);

        // act
        var response = await http.GetAsync("AMS");
        
        // assert 
        Assert.That(response, Is.Not.Null);
    }
}
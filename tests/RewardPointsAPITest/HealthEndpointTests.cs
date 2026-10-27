using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RewardPointsAPI_StandAlone.Tests
{
    [TestFixture]
    public class HealthEndpointTests
    {
        [Test]
        public async Task Health_ReturnsOkWithEmptyBody()
        {
            using var factory = new RewardPointsApiFactory();
            using var client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost"),
            });

            var response = await client.GetAsync("/api/Transactions/health");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(await response.Content.ReadAsStringAsync(), Is.Empty);
        }
    }
}

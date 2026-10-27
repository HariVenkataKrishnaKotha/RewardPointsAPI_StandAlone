using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RewardPointsAPI_StandAlone.Models;

namespace RewardPointsAPI_StandAlone.Tests
{
    // customerName and monthName are documented as optional:
    // omitted means "all customers" / "all months".
    [TestFixture]
    [SetCulture("en-US")] // month names are resolved with the current culture
    public class OptionalQueryParameterTests
    {
        private RewardPointsApiFactory _factory = null!;
        private HttpClient _client = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new RewardPointsApiFactory();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost"),
            });
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        private async Task<List<T>> GetListAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), await response.Content.ReadAsStringAsync());
            return (await response.Content.ReadFromJsonAsync<List<T>>())!;
        }

        [Test]
        public async Task RewardPoints_WithoutCustomerName_ReturnsEveryCustomerForTheMonth()
        {
            var result = await GetListAsync<RewardPoints>("/api/Transactions/rewardpoints?monthName=February");

            Assert.That(result.Select(r => (r.Customer, r.Month, r.Points)), Is.EquivalentTo(new[]
            {
                ("Jane Doe", 2, 250),
                ("Alex Jones", 2, 150),
            }));
        }

        [Test]
        public async Task RewardPoints_WithoutMonthName_ReturnsEveryMonthForTheCustomer()
        {
            var result = await GetListAsync<RewardPoints>("/api/Transactions/rewardpoints?customerName=Brian%20Paul");

            Assert.That(result.Select(r => (r.Customer, r.Month, r.Points)), Is.EquivalentTo(new[]
            {
                ("Brian Paul", 1, 0),
                ("Brian Paul", 3, 168),
            }));
        }

        [Test]
        public async Task RewardPoints_WithNoParameters_ReturnsEveryCustomerAndMonth()
        {
            var result = await GetListAsync<RewardPoints>("/api/Transactions/rewardpoints");

            Assert.That(result.Select(r => (r.Customer, r.Month, r.Points)), Is.EquivalentTo(new[]
            {
                ("Jane Doe", 1, 120),
                ("Jane Doe", 2, 250),
                ("Alex Jones", 2, 150),
                ("Alex Jones", 3, 50),
                ("Brian Paul", 1, 0),
                ("Brian Paul", 3, 168),
            }));
        }

        [Test]
        public async Task RewardPointsTotal_WithoutCustomerName_ReturnsEveryCustomer()
        {
            var result = await GetListAsync<RewardPointsTotal>("/api/Transactions/rewardpoints/total");

            Assert.That(result.Select(r => (r.Customer, r.Points)), Is.EquivalentTo(new[]
            {
                ("Jane Doe", 370),
                ("Alex Jones", 200),
                ("Brian Paul", 168),
            }));
        }

        [Test]
        public async Task RewardPoints_WithBothParameters_StillFilters()
        {
            var result = await GetListAsync<RewardPoints>("/api/Transactions/rewardpoints?customerName=jane%20doe&monthName=january");

            Assert.That(result.Select(r => (r.Customer, r.Month, r.Points)), Is.EquivalentTo(new[]
            {
                ("Jane Doe", 1, 120),
            }));
        }
    }
}

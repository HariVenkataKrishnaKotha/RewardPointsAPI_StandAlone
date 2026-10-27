using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace RewardPointsAPI_StandAlone.Tests
{
    // Hosts the real API in memory with a fixed set of transactions, so the
    // tests exercise routing, model binding and validation as well as the controller.
    public class RewardPointsApiFactory : WebApplicationFactory<Program>
    {
        public static readonly IReadOnlyList<(string Customer, int Month, decimal Amount)> Transactions = new[]
        {
            ("Jane Doe", 1, 120m),   // 90
            ("Jane Doe", 1, 80m),    // 30
            ("Jane Doe", 2, 200m),   // 250
            ("Alex Jones", 2, 150m), // 150
            ("Alex Jones", 3, 90m),  // 40
            ("Alex Jones", 3, 60m),  // 10
            ("Brian Paul", 1, 36m),  // 0
            ("Brian Paul", 3, 135m), // 120
            ("Brian Paul", 3, 98m),  // 48
        };

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                // Replace appsettings.json so the tests do not depend on the sample data.
                config.Sources.Clear();

                var settings = new Dictionary<string, string?>();
                for (var i = 0; i < Transactions.Count; i++)
                {
                    settings[$"Transactions:{i}:Customer"] = Transactions[i].Customer;
                    settings[$"Transactions:{i}:Month"] = Transactions[i].Month.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    settings[$"Transactions:{i}:Amount"] = Transactions[i].Amount.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }

                config.AddInMemoryCollection(settings);
            });
        }
    }
}

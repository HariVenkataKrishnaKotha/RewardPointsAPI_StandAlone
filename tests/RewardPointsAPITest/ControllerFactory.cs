using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using RewardPointsAPI_StandAlone.Controllers;

namespace RewardPointsAPI_StandAlone.Tests
{
    // Builds a TransactionsController over an in-memory "Transactions" section,
    // laid out the same way appsettings.json is.
    internal static class ControllerFactory
    {
        public static TransactionsController Create(params (string Customer, int Month, decimal Amount)[] transactions)
        {
            var settings = new Dictionary<string, string?>();
            for (var i = 0; i < transactions.Length; i++)
            {
                settings[$"Transactions:{i}:Customer"] = transactions[i].Customer;
                settings[$"Transactions:{i}:Month"] = transactions[i].Month.ToString(System.Globalization.CultureInfo.InvariantCulture);
                settings[$"Transactions:{i}:Amount"] = transactions[i].Amount.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            return new TransactionsController(NullLogger<TransactionsController>.Instance, configuration);
        }
    }
}

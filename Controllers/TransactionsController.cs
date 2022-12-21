
using Microsoft.AspNetCore.Mvc;
using RewardPointsAPI_StandAlone.Models;

namespace RewardPointsAPI_StandAlone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TransactionsController> logger;
        public TransactionsController(ILogger<TransactionsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            try
            {
                var transactionsSection = _configuration.GetSection("Transactions");
                var transactions = transactionsSection.GetChildren()
                    .Select(s => new Transaction
                    {
                        Customer = s["Customer"],
                        Month = int.Parse(s["Month"]),
                        Amount = decimal.Parse(s["Amount"])
                    })
                    .ToList();
                _logger.LogInformation("Returning all transactions.");
                return transactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting transactions.");
                throw;
            }
        }

        [HttpGet("rewardpoints")]
        public IEnumerable<RewardPoints> GetRewardPoints([FromQuery] string customerName, [FromQuery] string monthName)
        {
            try
            {
                var transactionsSection = _configuration.GetSection("Transactions");
                var transactions = transactionsSection.GetChildren()
                    .Select(s => new Transaction
                    {
                        Customer = s["Customer"],
                        Month = int.Parse(s["Month"]),
                        Amount = decimal.Parse(s["Amount"])
                    })
                    .ToList();

                if (!string.IsNullOrEmpty(customerName))
                {
                    transactions = transactions.Where(t => t.Customer.ToLower() == customerName.ToLower()).ToList();
                }

                if (!string.IsNullOrEmpty(monthName))
                {
                    transactions = transactions.Where(t => t.GetMonthName.ToLower() == monthName.ToLower()).ToList();
                }

                var rewardPoints = transactions
                    .GroupBy(t => t.Month)
                    .Select(g => new RewardPoints
                    {
                        Month = g.Key,
                        Customer = g.First().Customer,
                        Points = g.Sum(t => GetPoints(t.Amount))
                    })
                    .ToList();

                _logger.LogInformation("Returning reward points for the customer: " + customerName + " in the month: " + monthName);
                return rewardPoints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting reward points for the customer: " + customerName + " in the month: " + monthName);
                throw;
            }
        }

        [HttpGet("rewardpoints/total")]
        public IEnumerable<RewardPointsTotal> GetTotalRewardPoints([FromQuery] string customerName)
        {
            try
            {
                var transactionsSection = _configuration.GetSection("Transactions");
                var transactions = transactionsSection.GetChildren()
                    .Select(s => new Transaction
                    {
                        Customer = s["Customer"],
                        Month = int.Parse(s["Month"]),
                        Amount = decimal.Parse(s["Amount"])
                    })
                    .ToList();

                if (!string.IsNullOrEmpty(customerName))
                {
                    transactions = transactions.Where(t => t.Customer.ToLower() == customerName.ToLower()).ToList();
                }

                var rewardPoints = transactions
                    .GroupBy(t => t.Customer)
                    .Select(g => new RewardPointsTotal
                    {
                        Customer = g.Key,
                        Points = g.Sum(t => GetPoints(t.Amount))
                    })
                    .ToList();

                _logger.LogInformation("Returning total reward points for the customer:" + customerName);
                return rewardPoints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting total reward points for the customer:" + customerName);
                throw;
            }
        }

        private int GetPoints(decimal amount)
        {
            var points = 0;

            if (amount > 50)
            {
                points += (int)(amount - 50);
            }

            if (amount > 100)
            {
                points += (int)(amount - 100);
            }

            return points;
        }
    }
}

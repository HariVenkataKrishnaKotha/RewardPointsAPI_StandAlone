using RewardPointsAPI_StandAlone.Models;

namespace RewardPointsAPI_StandAlone.Tests
{
    [TestFixture]
    [SetCulture("en-US")] // month names are resolved with the current culture
    public class MonthlyRewardPointsTests
    {
        private static IEnumerable<(string? Customer, int Month, int Points)> Rows(IEnumerable<RewardPoints> result) =>
            result.Select(r => (r.Customer, r.Month, r.Points));

        [Test]
        public void TwoCustomersInTheSameMonth_GetSeparateTotals()
        {
            var controller = ControllerFactory.Create(
                ("Jane Doe", 2, 200m),   // 250 points
                ("Alex Jones", 2, 150m)); // 150 points

            var result = controller.GetRewardPoints(null, "February");

            Assert.That(Rows(result), Is.EquivalentTo(new[]
            {
                ("Jane Doe", 2, 250),
                ("Alex Jones", 2, 150),
            }));
        }

        [Test]
        public void SameCustomerAcrossMonths_GetsOneRowPerMonth()
        {
            var controller = ControllerFactory.Create(
                ("Jane Doe", 1, 120m),    // 90 points
                ("Jane Doe", 1, 80m),     // 30 points
                ("Jane Doe", 2, 200m),    // 250 points
                ("Alex Jones", 2, 150m)); // other customer, filtered out

            var result = controller.GetRewardPoints("Jane Doe", null);

            Assert.That(Rows(result), Is.EquivalentTo(new[]
            {
                ("Jane Doe", 1, 120),
                ("Jane Doe", 2, 250),
            }));
        }
    }
}

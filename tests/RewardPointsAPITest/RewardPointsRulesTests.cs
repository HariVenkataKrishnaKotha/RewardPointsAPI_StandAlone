namespace RewardPointsAPI_StandAlone.Tests
{
    // Points rule: 1 point per whole dollar over $50, plus 1 more point
    // per whole dollar over $100, in a single transaction.
    [TestFixture]
    [SetCulture("en-US")] // month names are resolved with the current culture
    public class RewardPointsRulesTests
    {
        [TestCase(0, 0)]
        [TestCase(50, 0)]
        [TestCase(51, 1)]
        [TestCase(100, 50)]
        [TestCase(101, 52)]
        [TestCase(120, 90)]
        public void SingleTransaction_EarnsPointsPerTier(decimal amount, int expectedPoints)
        {
            var controller = ControllerFactory.Create(("Sample Customer", 1, amount));

            var result = controller.GetTotalRewardPoints("Sample Customer").ToList();

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Points, Is.EqualTo(expectedPoints));
        }

        [Test]
        public void PointsAreSummedAcrossTransactionsInTheSameMonth()
        {
            // $120 -> 90 points, $80 -> 30 points
            var controller = ControllerFactory.Create(
                ("Sample Customer", 1, 120m),
                ("Sample Customer", 1, 80m));

            var result = controller.GetRewardPoints("Sample Customer", "January").ToList();

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Points, Is.EqualTo(120));
        }
    }
}

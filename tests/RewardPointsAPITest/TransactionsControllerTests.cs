using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RewardPointsAPI_StandAlone.Controllers;
using RewardPointsAPI_StandAlone.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace RewardPointsAPI_StandAlone.Tests
{
    [TestFixture]
    public class TransactionsControllerTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<ILogger<TransactionsController>> _mockLogger;
        private TransactionsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<TransactionsController>>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Create the MemoryConfigurationSource object
            var memoryConfigurationSource = new MemoryConfigurationSource();

            // Create the first IConfigurationSection object
            var transaction1 = new ConfigurationRoot(new List<IConfigurationProvider>()
            {
                new MemoryConfigurationProvider(memoryConfigurationSource)
            })
                .GetSection("Transaction1");
            transaction1["Customer"] = "Hari Kotha";
            transaction1["Month"] = "1";
            transaction1["Amount"] = "120";

            // Create the second IConfigurationSection object
            var transaction2 = new ConfigurationRoot(new List<IConfigurationProvider>()
            {
                new MemoryConfigurationProvider(memoryConfigurationSource)
            })
                .GetSection("Transaction2");
            transaction2["Customer"] = "Hari Kotha";
            transaction2["Month"] = "1";
            transaction2["Amount"] = "80";
            
            // Create the third IConfigurationSection object
            var transaction3 = new ConfigurationRoot(new List<IConfigurationProvider>()
            {
                new MemoryConfigurationProvider(memoryConfigurationSource)
            })
                .GetSection("Transaction3");
            transaction3["Customer"] = "Hari Kotha";
            transaction3["Month"] = "3";
            transaction3["Amount"] = "200";

            // Create the fourth IConfigurationSection object
            var transaction4 = new ConfigurationRoot(new List<IConfigurationProvider>()
            {
                new MemoryConfigurationProvider(memoryConfigurationSource)
            })
                .GetSection("Transaction4");
            transaction4["Customer"] = "Alex Jones";
            transaction4["Month"] = "3";
            transaction4["Amount"] = "90";

            // Create the fifth IConfigurationSection object
            var transaction5 = new ConfigurationRoot(new List<IConfigurationProvider>()
            {
                new MemoryConfigurationProvider(memoryConfigurationSource)
            })
                .GetSection("Transaction3");
            transaction5["Customer"] = "Alex Jones";
            transaction5["Month"] = "2";
            transaction5["Amount"] = "137";

            // Initialize the transactions list with the IConfigurationSection objects
            var transactions = new List<IConfigurationSection>
    {
        transaction1,
        transaction2,
        transaction3,
        transaction4,
        transaction5
    };

            // Setup the mock IConfiguration object
            _mockConfiguration.Setup(c => c.GetSection("Transactions").GetChildren())
                .Returns(transactions);

            // Create the TransactionsController instance
            _controller = new TransactionsController(_mockLogger.Object, _mockConfiguration.Object);
        }


        [Test]
        public void Get_ReturnsListOfTransactions()
        {
            // Arrange
            
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Transaction>>(result);
            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.That(result.ElementAt(0).Customer, Is.EqualTo("Hari Kotha"));
            Assert.That(result.ElementAt(0).Month, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Amount, Is.EqualTo(120));
            Assert.That(result.ElementAt(3).Customer, Is.EqualTo("Alex Jones"));
            Assert.That(result.ElementAt(3).Month, Is.EqualTo(3));
            Assert.That(result.ElementAt(3).Amount, Is.EqualTo(90));
        }
        [Test]
        public void GetRewardPoints_ReturnsListOfRewardPoints()
        {
            // Arrange
            /*var transactions = new List<Transaction>
            {
                new Transaction { Customer = "John Smith", Month = 1, Amount = 100 },
                new Transaction { Customer = "John Smith", Month = 2, Amount = 200 },
                new Transaction { Customer = "Jane Doe", Month = 1, Amount = 300 }
            };
            _mockConfiguration.Setup(c => c.GetSection("Transactions").Get<List<Transaction>>())
                .Returns(transactions);*/

            // Act
            var result = _controller.GetRewardPoints("Hari Kotha", "January");

            // Assert
            Assert.IsInstanceOf<IEnumerable<RewardPoints>>(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Customer, Is.EqualTo("Hari Kotha"));
            Assert.That(result.ElementAt(0).Month, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Points, Is.EqualTo(120));
        }

        [Test]
        public void GetTotalRewardPoints_ReturnsListOfTotalRewardPoints()
        {
            // Arrange
            /*var transactions = new List<Transaction>
            {
                new Transaction { Customer = "John Smith", Month = 1, Amount = 100 },
                new Transaction { Customer = "John Smith", Month = 2, Amount = 200 },
                new Transaction { Customer = "Jane Doe", Month = 1, Amount = 300 }
            };
            _mockConfiguration.Setup(c => c.GetSection("Transactions").Get<List<Transaction>>())
                .Returns(transactions);*/

            // Act
            var result = _controller.GetTotalRewardPoints("Alex Jones");

            // Assert
            Assert.IsInstanceOf<IEnumerable<RewardPointsTotal>>(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Customer, Is.EqualTo("Alex Jones"));
            Assert.That(result.ElementAt(0).Points, Is.EqualTo(164));
        }
    }
}


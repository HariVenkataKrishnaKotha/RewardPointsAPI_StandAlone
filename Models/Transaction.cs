using System;
using System.Globalization;

namespace RewardPointsAPI_SA.Models
{
    // Transaction.cs

    public class Transaction
    {
        public string? Customer { get; set; }
        public int Month { get; set; }
        public int Amount { get; set; }
    }
}

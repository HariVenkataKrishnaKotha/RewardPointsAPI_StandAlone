using System;
using System.Globalization;

namespace RewardPointsAPI_StandAlone.Models
{
    // Transaction.cs

    public class Transaction
    {
        public string? Customer { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public string GetMonthName => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
    }
}

using System;
using System.Globalization;

namespace RewardPointsAPI_SA.Models
{
    // RewardPoints.cs

    public class RewardPoints
    {
        public string? Customer { get; set; }
        public int Month { get; set; }
        public int Points { get; set; }
        public string MonthName => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
    }
}

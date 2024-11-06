namespace TPR.MVC.Helpers
{
    public static class TimeAgoHelper
    {
        public static string TimeAgo(DateTime dateTime)
        {
            TimeSpan timeSince = DateTime.Now.Subtract(dateTime);

            if (timeSince.TotalSeconds <= 60) // less than or equal to a minute
            {
                return "New";
            }
            else if (timeSince.TotalMinutes < 60) // less than an hour
            {
                return $"{(int)timeSince.TotalMinutes} min ago";
            }
            else if (timeSince.TotalHours < 24) // less than a day
            {
                int hoursAgo = (int)timeSince.TotalHours;
                return $"{hoursAgo} {(hoursAgo == 1 ? "hour" : "hours")} ago";
            }
            else if (timeSince.TotalDays < 30) // less than a month
            {
                int daysAgo = (int)timeSince.TotalDays;
                return $"{daysAgo} {(daysAgo == 1 ? "day" : "days")} ago";
            }
            else if (timeSince.TotalDays < 365) // less than a year
            {
                int monthsAgo = (int)Math.Round(timeSince.TotalDays / 30.0);
                return $"{monthsAgo} {(monthsAgo == 1 ? "month" : "months")} ago";
            }
            else // more than a year
            {
                int yearsAgo = (int)Math.Round(timeSince.TotalDays / 365.0);
                return $"{yearsAgo} {(yearsAgo == 1 ? "year" : "years")} ago";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;

namespace AlexisCorePro.Infrastructure.Helpers
{
    public class DateHelper
    {
        public static DateRange GetLast7DaysDates(DateTime Today)
        {
            return new DateRange(Today.Subtract(new TimeSpan(7, 0, 0, 0)), Today);
        }

        public static DateRange GetThisMonthDates(DateTime Today)
        {
            return new DateRange(new DateTime(Today.Year, Today.Month, 1), Today);
        }

        public static DateRange GetThisWeekDates(DateTime Today)
        {
            return new DateRange(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek), Today);
        }

        public static DateRange GetFirstAndLastDay(DateTime Today)
        {
            return new DateRange(new DateTime(Today.Year, Today.Month, 1), new DateTime(Today.Year, Today.Month, DateTime.DaysInMonth(Today.Year, Today.Month)));
        }

        public static DateTime GetFirstDayForWeek(DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Sunday)
            {
                return day.AddDays(-6);
            }

            return day.AddDays(-(int)day.DayOfWeek + 1);
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static DateTime ParseSixDigitDate(string date)
        {
            int day = int.Parse(date.Substring(0, 2));
            int month = int.Parse(date.Substring(2, 2));
            int year = int.Parse(date.Substring(4, 2));
            int fullYear = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(year);

            return new DateTime(fullYear, month, day);
        }
    }

    public class DateRange
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public DateRange(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}

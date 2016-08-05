using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.Date;

namespace ValueObjects.Tests
{
    [TestClass]
    public class CalendarMonthTests
    {
        [TestMethod]
        public void DateTime_can_be_converted_to_a_CalendarMonth()
        {
            var calendarMonth = new CalendarMonth(DateTime.Now);
            string testString = DateTime.Now.ToString("yyyy-MM", CultureInfo.InvariantCulture);
            string calendarMonthString = calendarMonth.ToString();
            calendarMonthString.ShouldBe(testString);
        }

        [TestMethod]
        public void String_can_be_parsed_to_a_CalendarMonth()
        {
            var calendarMonth = CalendarMonth.Parse("1/2015");
            string calendarMonthString = calendarMonth.ToString();
            calendarMonthString.ShouldBe("2015-01");
        }

        [TestMethod]
        public void Can_get_last_day_of_month()
        {
            var month = new CalendarMonth(2016, 08);
            DateTime lastDay = month.GetLastDayOfMonth();
            lastDay.ShouldBe(new DateTime(2016, 08, 31));
        }
    }
}
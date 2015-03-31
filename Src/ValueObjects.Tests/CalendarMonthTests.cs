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
            string testString = DateTime.Now.ToString("MM/yyyy", CultureInfo.InvariantCulture);
            string calendarMonthString = calendarMonth.ToString();
            calendarMonthString.ShouldBe(testString);
        }

        [TestMethod]
        public void String_can_be_parsed_to_a_CalendarMonth()
        {
            var calendarMonth = CalendarMonth.Parse("1/2015");
            string calendarMonthString = calendarMonth.ToString();
            calendarMonthString.ShouldBe("01/2015");
        }
    }
}
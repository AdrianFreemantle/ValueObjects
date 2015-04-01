using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.People;

namespace ValueObjects.Tests
{
    [TestClass]
    public class DateOfBirthTests
    {
        [TestMethod]
        public void Create_from_year_month_day_strings_1978_birth_date()
        {
            DateTime dateOfBirth = new DateOfBirth("1978", "08", "03");
            dateOfBirth.ShouldBe(new DateTime(1978, 08, 03));
        }

        [TestMethod]
        public void Create_from_year_month_day_strings_78_birth_date()
        {
            DateTime dateOfBirth = new DateOfBirth("78", "08", "03");
            dateOfBirth.ShouldBe(new DateTime(1978, 08, 03));
        }

        [TestMethod]
        public void Create_from_year_month_day_strings_2000_birth_date()
        {
            DateTime dateOfBirth = new DateOfBirth("2000", "08", "03");
            dateOfBirth.ShouldBe(new DateTime(2000, 08, 03));
        }

        [TestMethod]
        public void Create_from_year_month_day_strings_00_birth_date()
        {
            DateTime dateOfBirth = new DateOfBirth("00", "08", "03");
            dateOfBirth.ShouldBe(new DateTime(2000, 08, 03));
        }

        [TestMethod]
        public void Create_from_year_month_day_strings_01_birth_date()
        {
            DateTime dateOfBirth = new DateOfBirth("01", "08", "03");
            dateOfBirth.ShouldBe(new DateTime(2001, 08, 03));
        }

        [TestMethod]
        public void Age_next_birthday_with_birthday_tomorrow()
        {
            var tenthBirthday = DateTime.Now.AddYears(-10).AddDays(1); //ten years ago, tommorrow

            var dateOfBirth = new DateOfBirth(tenthBirthday);
            int ageNextBirthday = dateOfBirth.AgeNextBirthday();
            ageNextBirthday.ShouldBe(10);
        }

        [TestMethod]
        public void Age_next_birthday_with_birthdate_past_in_current_year()
        {
            var tenthBirthday = DateTime.Now.AddYears(-10).AddDays(-1); //ten years ago, yesterday

            var dateOfBirth = new DateOfBirth(tenthBirthday);
            int ageNextBirthday = dateOfBirth.AgeNextBirthday();
            ageNextBirthday.ShouldBe(11);
        }

        [TestMethod]
        public void Age_next_birthday_with_birthday_today()
        {
            var tenthBirthday = DateTime.Now.AddYears(-10); //ten years ago, today

            var dateOfBirth = new DateOfBirth(tenthBirthday);
            int ageNextBirthday = dateOfBirth.AgeNextBirthday();
            ageNextBirthday.ShouldBe(11);
        }

        [TestMethod]
        public void Age_next_birthday_first_birthday()
        {
            var firstBirthday = DateTime.Now.AddYears(-1).AddMonths(1).AddDays(-1); //upcomming first birthday

            var dateOfBirth = new DateOfBirth(firstBirthday);
            int ageNextBirthday = dateOfBirth.AgeNextBirthday();
            ageNextBirthday.ShouldBe(1);
        }

        [TestMethod]
        [ExpectedException(typeof(PersonAgeException))]
        public void Age_next_birthday_currently_unborn()
        {
            var dueDate = DateTime.Now.AddYears(1).AddMonths(1); //birthdate is one year and one month in the future

            var dateOfBirth = new DateOfBirth(dueDate);
            dateOfBirth.AgeNextBirthday();
        }
    }
}
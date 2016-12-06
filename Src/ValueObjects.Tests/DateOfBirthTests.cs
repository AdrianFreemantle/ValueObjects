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
    }
}
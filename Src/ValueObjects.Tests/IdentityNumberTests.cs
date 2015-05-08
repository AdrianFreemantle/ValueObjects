using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.People;

namespace ValueObjects.Tests
{
    [TestClass]
    public class IdentityNumberTests
    {
        [TestMethod]
        public void Sample_test()
        {
            IdentityNumber.IsValid("8905151430088");
            IdentityNumber.IsValid("8905151430088");
            IdentityNumber.IsValid("7002222412087");
            IdentityNumber.IsValid("7808035176089");
            IdentityNumber.IsValid("7503035682087");
        }

        [TestMethod]
        public void Valid_identity_number()
        {
            IdentityNumber.IsValid("7808035176089").ShouldBe(true);
        }

        [TestMethod]
        public void Invalid_birth_date()
        {
            IdentityNumber.IsValid("7888835176089").ShouldBe(false);
        }

        [TestMethod]
        public void Invalid_length()
        {
            IdentityNumber.IsValid("780803517608933").ShouldBe(false);
        }

        [TestMethod]
        public void Invalid_characters()
        {
            IdentityNumber.IsValid("78080351abcde").ShouldBe(false);
        }

        [TestMethod]
        public void Valid_identity_number_date_of_birth()
        {
            var identityNumber = new IdentityNumber("7808035176089");
            identityNumber.DateOfBirth.ShouldBe(new DateOfBirth(1978, 08, 03));
        }

        [TestMethod]
        public void Valid_identity_number_gender()
        {
            var identityNumber = new IdentityNumber("7808035176089");
            identityNumber.Gender.ShouldBe(Gender.Male);
        }


        [TestMethod]
        public void Valid_identity_number_5008150764080() //test case from identity number that is correct but failed validation
        {
            IdentityNumber.IsValid("5008150764080").ShouldBe(true);
        }
    }
}
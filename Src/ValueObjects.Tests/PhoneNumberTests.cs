using System;
using System.Management.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.ContactDetail;

namespace ValueObjects.Tests
{
    [TestClass]
    public class PhoneNumberTests
    {
        [TestMethod]
        public void Phone_number_with_special_characters_is_accepted()
        {
            TelephoneNumber.Parse("0845140900-1");
            TelephoneNumber.Parse("084-514-0900");
            TelephoneNumber.Parse("(084) 514-0900");
            TelephoneNumber.Parse("084-5140900");
            TelephoneNumber.Parse("084 514 2072-1");
            TelephoneNumber.Parse("084 514 2072 x 134");
            TelephoneNumber.Parse("084 514 2072 Ext 134");
            TelephoneNumber.Parse("084 514 2072/134");
            TelephoneNumber.Parse("084 514 2072/134");
        }

        [TestMethod]
        public void Local_phone_number_format_is_accepted()
        {
            var phoneNumber = TelephoneNumber.FromString("0845140900");
            phoneNumber.ToString().ShouldBe("0845140900");
        }

        [TestMethod]
        public void South_African_international_phone_number_format_is_accepted()
        {
            var phoneNumber = TelephoneNumber.FromString("+27845140900");
            phoneNumber.ToString().ShouldBe("0845140900");
        }

        [TestMethod]
        public void Cellular_numbers_can_receive_sms_messages()
        {
            var phoneNumber = TelephoneNumber.FromString("+27845140900");
            phoneNumber.CanRecieveSms().ShouldBe(true);
        }

        [TestMethod]
        public void Non_cellular_numbers_cannot_receive_sms_messages()
        {
            var phoneNumber = TelephoneNumber.FromString("+27113332163");
            phoneNumber.CanRecieveSms().ShouldBe(false);
        }

        [TestMethod]
        public void Local_numbers_must_be_no_more_than_ten_digits_long()
        {
            try
            {
                TelephoneNumber.FromString("08451409001");
                Assert.Fail("The expected exception was not thrown");
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType<InvalidPhoneNumberException>();
            }
        }

        [TestMethod]
        public void Local_numbers_must_be_no_less_than_ten_digits_long()
        {
            try
            {
                TelephoneNumber.FromString("084514090");
                Assert.Fail("The expected exception was not thrown");
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType<InvalidPhoneNumberException>();
            }
        }

        [TestMethod]
        public void South_African_international_phone_numbers_must_be_no_more_than_eleven_digits_long()
        {
            try
            {
                TelephoneNumber.FromString("+271133321632");
                Assert.Fail("The expected exception was not thrown");
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType<InvalidPhoneNumberException>();
            }
        }

        [TestMethod]
        public void South_African_international_phone_numbers_must_be_no_less_than_eleven_digits_long()
        {
            try
            {
                TelephoneNumber.FromString("+2711333216");
                Assert.Fail("The expected exception was not thrown");
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType<InvalidPhoneNumberException>();
            }
        }

        [TestMethod]
        public void A_phone_number_may_contain_formatting_characters()
        {
            TelephoneNumber.FromString("+27-11-333-2163").ToString().ShouldBe("0113332163");
            TelephoneNumber.FromString("+27 11 333 2163").ToString().ShouldBe("0113332163");
            TelephoneNumber.FromString("(011)-333-2163").ToString().ShouldBe("0113332163");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPhoneNumberException))]
        public void Area_code_must_have_first_digit_as_zero()
        {
            TelephoneNumber.FromString("1605693825");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPhoneNumberException))]
        public void Area_code_may_not_have_second_digit_as_zero()
        {
            TelephoneNumber.FromString("0005693825");
        }

        [TestMethod]
        public void Area_code_may_have_thrid_digit_as_zero()
        {
            TelephoneNumber.FromString("0605693825").ToString().ShouldBe("0605693825");
        }

        [TestMethod]
        public void Area_code_06_can_receive_sms()
        {
            TelephoneNumber.FromString("0605693825").CanRecieveSms().ShouldBe(true);
        }
    }
}
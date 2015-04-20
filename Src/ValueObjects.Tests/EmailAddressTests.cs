using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.ContactDetail;

namespace ValueObjects.Tests
{
    [TestClass]
    public class EmailAddressTests
    {
        [TestMethod]
        public void Can_parse_standard_address()
        {
            EmailAddress.Parse("adrian@gmail.com").ToString().ShouldBe("adrian@gmail.com");
        }

        [TestMethod]
        public void Can_parse_dirty_address()
        {
            EmailAddress.Parse("adrian 2 @@ gmail.com,").ToString().ShouldBe("adrian2@gmail.com");
        }

        [TestMethod]
        public void Can_clean_gmail_address_missing_domain_suffix()
        {
            EmailAddress.Parse("adrian@gmail").ToString().ShouldBe("adrian@gmail.com");
        }

        [TestMethod]
        public void Can_fix_case_where_2_was_entered_instead_of_at_symbol()
        {
            EmailAddress.Parse("adrian.freemantle2gmail.com").ToString().ShouldBe("adrian.freemantle@gmail.com");
        }

        [TestMethod]
        public void Can_fix_double_at_symbol()
        {
            EmailAddress.Parse("adrian.freemantle@@gmail.com").ToString().ShouldBe("adrian.freemantle@gmail.com");
        }

        [TestMethod]
        public void Validation_is_not_case_sensitive()
        {
            EmailAddress.Parse("AdrianFreemantle@gmail.com");
        }
    }
}
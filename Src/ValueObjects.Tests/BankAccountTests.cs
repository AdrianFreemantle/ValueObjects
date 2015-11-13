using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.Finance;

namespace ValueObjects.Tests
{
    [TestClass]
    public class BankAccountTypeTests
    {
        [TestMethod]
        public void Can_convert_to_string()
        {
            BankAccountType accountType = BankAccountType.Parse("Current");
            string description = accountType;

            accountType.ToString().ShouldBe("Current");
            description.ShouldBe("Current");
        }
    }
}
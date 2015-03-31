using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.Finance;

namespace ValueObjects.Tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void Subtraction_with_a_positive_balance()
        {
            Balance result = Money.FromAmount(1000) - Money.FromAmount(500);
            result.ToString().ShouldBe("R500.00");
        }

        [TestMethod]
        public void Subtraction_with_a_negative_balance()
        {
            Balance result = Money.FromAmount(500) - Money.FromAmount(10000);
            result.ToString().ShouldBe("-R9,500.00");
        }

        [TestMethod]
        public void Crediting_a_negative_balance_results_in_a_positive_balance()
        {
            Balance result = Balance.FromAmount(-100) + Money.FromAmount(1000);
            result.ToString().ShouldBe("R900.00");
        }

        [TestMethod]
        public void Debiting_a_positive_balance_results_in_a_negative_balance()
        {
            Balance result = Balance.FromAmount(100) - Money.FromAmount(1000);
            result.ToString().ShouldBe("-R900.00");
        }

        [TestMethod]
        public void Adding_positive_balances_results_in_a_positive_balance()
        {
            Balance result = Balance.FromAmount(100) + Balance.FromAmount(1000);
            result.ToString().ShouldBe("R1,100.00");
        }

        [TestMethod]
        public void Adding_negative_balances_results_in_a_negative_balance()
        {
            Balance result = Balance.FromAmount(-100) + Balance.FromAmount(-1000);
            result.ToString().ShouldBe("-R1,100.00");
        }
    }
}
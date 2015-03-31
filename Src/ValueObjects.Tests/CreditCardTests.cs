using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.Finance;

namespace ValueObjects.Tests
{
    [TestClass]
    public class CreditCardTests
    {
        [TestMethod]
        public void Visa_credit_card_can_be_verified()
        {
            var creditCard = new CreditCard("4787692135549025");
            Assert.AreEqual(creditCard.GetCardType(), CreditCardType.VisaCard);
        }
    }
}
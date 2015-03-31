using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace ValueObjects.Finance
{
    [DataContract(Name = "CreditCard", Namespace = "Finance")]
    public struct CreditCard : IEquatable<CreditCard>
    {      
        private const string RegexCardNumberPattern = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";
        private static readonly Regex CardNumberPattern = new Regex(RegexCardNumberPattern); 

        [DataMember(Name = "cardNumber")]
        private readonly string cardNumber;

        public CreditCard(string cardNumber)
        {
            if(!IsValid(cardNumber))
                throw new CreditCardNumberException(String.Format("Credit card cardNumber [{0}] is invalid.", cardNumber));

            this.cardNumber = GetCleanCardNumber(cardNumber);
        }

        public static CreditCard Parse(string s)
        {
            return new CreditCard(s);
        }

        public static bool TryParse(string s, out CreditCard creditCard)
        {
            creditCard = new CreditCard();
            
            try
            {
                creditCard = new CreditCard(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsValid(string cardNumber)
        {
            if (String.IsNullOrWhiteSpace(cardNumber))
                return false;

            var trimmedCardNumber = GetCleanCardNumber(cardNumber);

            return CardNumberPattern.IsMatch(trimmedCardNumber);
        }

        public CreditCardType GetCardType()
        {
            return new CreditCardType(cardNumber);
        }

        private static string GetCleanCardNumber(string cardnumber)
        {
            return cardnumber.Replace(" ", String.Empty).Replace("-", String.Empty).Trim();
        }

        public override int GetHashCode()
        {
            return (cardNumber != null ? cardNumber.GetHashCode() : 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is CreditCard && Equals((CreditCard)obj);
        }

        public bool Equals(CreditCard other)
        {
            return other.cardNumber == cardNumber;
        }

        public static bool operator ==(CreditCard left, CreditCard right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CreditCard left, CreditCard right)
        {
            return !Equals(left, right);
        }

        public static implicit operator string(CreditCard cardType)
        {
            return cardType.cardNumber;
        }

        public override string ToString()
        {
            return cardNumber;
        }
    }
}
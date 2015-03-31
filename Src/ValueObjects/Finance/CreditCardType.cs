using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace ValueObjects.Finance
{
    [DataContract(Name = "CreditCardType", Namespace = "Finance")]
    public struct CreditCardType : IEquatable<CreditCardType>, IEnumerable<CreditCardType>
    {
        enum CardTypes
        {
            [Description("")]
            Unknown = 0,

            [Description("American Express Card")]
            AmericanExpressCard = 1,

            [Description("Visa Card")]
            VisaCard = 2,

            [Description("Dinners Club Card")]
            DinnersClubCard = 3,

            [Description("Master Card")]
            MasterCard = 4,

            [Description("Discover Card")]
            DiscoverCard = 5,

            [Description("Japan Credit Bureau Card")]
            JapanCreditBureauCard = 6
        }

        private const string RegExVisa = @"^4[0-9]{12}(?:[0-9]{3})?$";
        private const string RegExMasterCard = @"^5[1-5][0-9]{14}$";
        private const string RegExAmericanExpress = @"^3[47][0-9]{13}$";
        private const string RegExDinersClub = @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$";
        private const string RegExDiscover = @"^6(?:011|5[0-9]{2})[0-9]{12}$";
        private const string RegExJcb = @"^(?:2131|1800|35\d{3})\d{11}$";

        [DataMember(Name = "cardType")]
        private readonly CardTypes cardType;

        public static readonly CreditCardType Empty = new CreditCardType(CardTypes.Unknown); 
        public static readonly CreditCardType AmericanExpressCard = new CreditCardType(CardTypes.AmericanExpressCard); 
        public static readonly CreditCardType VisaCard = new CreditCardType(CardTypes.VisaCard); 
        public static readonly CreditCardType DinnersClubCard = new CreditCardType(CardTypes.DinnersClubCard); 
        public static readonly CreditCardType MasterCard = new CreditCardType(CardTypes.MasterCard);
        public static readonly CreditCardType DiscoverCard = new CreditCardType(CardTypes.DiscoverCard); 
        public static readonly CreditCardType JapanCreditBureauCard = new CreditCardType(CardTypes.JapanCreditBureauCard); 

        public CreditCardType(string cardNumber)
        {
            cardType = DetermineCreditCardType(cardNumber);
        }

        private CreditCardType(CardTypes cardType)
        {
            this.cardType = cardType;
        }

        private static CardTypes DetermineCreditCardType(string creditCardNumber)
        {
            if (String.IsNullOrWhiteSpace(creditCardNumber))
                return CardTypes.Unknown;

            string trimmedCardNumber = creditCardNumber.Replace(" ", String.Empty).Replace("-", String.Empty).Trim();

            if (trimmedCardNumber.StartsWith("4") && Regex.IsMatch(trimmedCardNumber, RegExVisa))
                return CardTypes.VisaCard;

            if (trimmedCardNumber.StartsWith("5") && Regex.IsMatch(trimmedCardNumber, RegExMasterCard))
                return CardTypes.MasterCard;

            if (trimmedCardNumber.StartsWith("3") && Regex.IsMatch(trimmedCardNumber, RegExAmericanExpress))
                return CardTypes.AmericanExpressCard;

            if (trimmedCardNumber.StartsWith("3") && Regex.IsMatch(trimmedCardNumber, RegExDinersClub))
                return CardTypes.DinnersClubCard;

            if (trimmedCardNumber.StartsWith("6") && Regex.IsMatch(trimmedCardNumber, RegExDiscover))
                return CardTypes.DiscoverCard;

            if (Regex.IsMatch(trimmedCardNumber, RegExJcb))
                return CardTypes.JapanCreditBureauCard;

            return CardTypes.Unknown;
        }

        public override int GetHashCode()
        {
            return cardType.GetHashCode();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is CreditCardType && Equals((CreditCardType)obj);
        }

        public bool Equals(CreditCardType other)
        {
            return other.cardType == cardType;
        }

        public static bool operator ==(CreditCardType left, CreditCardType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CreditCardType left, CreditCardType right)
        {
            return !Equals(left, right);
        }

        public static implicit operator string(CreditCardType cardType)
        {
            return cardType.cardType.GetDescription();
        }

        public IEnumerator<CreditCardType> GetEnumerator()
        {
            return ObjectFactory.CreateInstances<CreditCardType, CardTypes>().GetEnumerator();
        }

        public override string ToString()
        {
            return cardType.GetDescription();
        }
    }
}


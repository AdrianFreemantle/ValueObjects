using System;
using System.Runtime.Serialization;

namespace ValueObjects.Finance
{
    //todo support multi currency types

    [DataContract(Name = "Money", Namespace = "Finance")]
    public struct Money : IEquatable<Money>
    {
        [DataMember(Name = "amount")]
        private readonly decimal amount;

        public static Money Zero { get { return new Money(0); } }

        public Money(decimal amount)
            : this()
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException("amount", "A monetary amount may not be negative");

            this.amount = amount;
        }

        public static Money FromAmount(decimal amout)
        {
            return new Money(amout);
        }

        public int ValueInCents()
        {
            return (int)(amount * 100);
        }

        public override int GetHashCode()
        {
            return amount.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Money && Equals((Money)obj);
        }

        public bool Equals(Money other)
        {
            return other.amount == amount;
        }

        public static implicit operator decimal(Money money)
        {
            return money.amount;
        }

        public static Balance operator +(Money left, Money right)
        {
            return new Balance(left.amount + right.amount);
        }

        public static Balance operator -(Money left, Money right)
        {
            return new Balance(left.amount - right.amount);
        }

        public static bool operator ==(Money left, Money right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return ToString(FormatInfo.DefaultNumberFormat);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return String.Format(formatProvider, "{0:C}", amount);
        }
    }
}
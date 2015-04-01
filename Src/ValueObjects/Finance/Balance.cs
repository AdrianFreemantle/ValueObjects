using System;
using System.Runtime.Serialization;
using ValueObjects.ContactDetail;

namespace ValueObjects.Finance
{
    [DataContract(Name = "Balance", Namespace = "Finance")]
    public struct Balance : IEquatable<Balance>
    {
        [DataMember(Name = "balance")]
        private readonly decimal balance;

        public Balance(decimal balance)
            : this()
        {
            this.balance = balance;
        }

        public static Balance FromAmount(decimal amount)
        {
            return new Balance(amount);
        }

        public Balance Credit(Money amount)
        {
            return new Balance(balance + amount);
        }

        public Balance Debit(Money amount)
        {
            return new Balance(balance - amount);
        }

        public int ValueInCents()
        {
            return (int)(balance * 100);
        }

        public override int GetHashCode()
        {
            return balance.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Balance && Equals((Balance)obj);
        }

        public bool Equals(Balance other)
        {
            return other.balance == balance;
        }

        public static implicit operator decimal(Balance balance)
        {
            return balance.balance;
        }

        public static Balance operator +(Balance left, Balance right)
        {
            return new Balance(left.balance + right.balance);
        }

        public static Balance operator +(Balance left, Money right)
        {
            return left.Credit(right);
        }

        public static Balance operator -(Balance left, Money right)
        {
            return left.Debit(right);
        }

        public static bool operator ==(Balance left, Balance right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Balance left, Balance right)
        {
            return !Equals(left, right);
        }

        public static implicit operator string(Balance balance)
        {
            return balance.ToString();
        }

        public override string ToString()
        {
            return ToString(FormatInfo.DefaultNumberFormat);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return String.Format(formatProvider, "{0:C}", balance);
        }
    }
}
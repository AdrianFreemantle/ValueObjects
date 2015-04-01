using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ValueObjects.Finance
{
    [DataContract(Name = "BankAccountType", Namespace = "Finance")]
    public struct BankAccountType : IEquatable<BankAccountType>, IEnumerable<BankAccountType>
    {
        enum AccountType
        {
            [Description("")]
            Unknown = 0,
            Current = 1,
            Savings = 2,
            Transmission = 3,
        }

        [DataMember(Name = "type")]
        private readonly AccountType type;

        public static BankAccountType Unknown { get { return new BankAccountType(AccountType.Unknown); } }
        public static BankAccountType Current { get { return new BankAccountType(AccountType.Current); } }
        public static BankAccountType Savings { get { return new BankAccountType(AccountType.Savings); } }
        public static BankAccountType Transmission { get { return new BankAccountType(AccountType.Transmission); } }
        public static BankAccountType Cheque { get { return new BankAccountType(AccountType.Current); } }

        public BankAccountType(int accountTypeId)
        {
            type = (AccountType)accountTypeId;
        }

        private BankAccountType(AccountType type)
        {
            this.type = type;
        }

        public static BankAccountType Parse(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return Unknown;

            var trimed = s.Trim();

            if (trimed.Length == 1 && trimed.IsAllDigits())
            {
                return new BankAccountType(Int32.Parse(s));
            }

            if(trimed.StartsWith("S", StringComparison.InvariantCultureIgnoreCase))
                return Savings;

            if (trimed.StartsWith("T", StringComparison.InvariantCultureIgnoreCase))
                return Transmission;

            if (trimed.StartsWith("Cu", StringComparison.InvariantCultureIgnoreCase))
                return Current;

            if (trimed.StartsWith("Ch", StringComparison.InvariantCultureIgnoreCase))
                return Cheque;

            return Unknown;
        }

        public static bool TryParse(string s, out BankAccountType bankAccountType)
        {
            bankAccountType = new BankAccountType();

            try
            {
                bankAccountType = Parse(s);
                return bankAccountType != Unknown;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return type.GetHashCode();
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

            return obj is BankAccountType && Equals((BankAccountType)obj);
        }

        public bool Equals(BankAccountType other)
        {
            return other.type == type;
        }

        public static bool operator ==(BankAccountType left, BankAccountType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BankAccountType left, BankAccountType right)
        {
            return !Equals(left, right);
        }

        public static implicit operator int(BankAccountType bankAccount)
        {
            return (int)bankAccount.type;
        }

        public static implicit operator string(BankAccountType bankAccount)
        {
            return bankAccount.ToString();
        }

        public IEnumerator<BankAccountType> GetEnumerator()
        {
            return ObjectFactory.CreateInstances<BankAccountType, AccountType>().GetEnumerator();
        }

        public override string ToString()
        {
            return type.GetDescription();
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace ValueObjects.Finance
{
    [DataContract(Name = "BankAccount", Namespace = "Finance")]
    public struct BankAccount : IEquatable<BankAccount>
    {
        [DataMember(Name = "accountNumber")]
        private readonly string accountNumber;

        [DataMember(Name = "branchCode")]
        private readonly string branchCode;

        [DataMember(Name = "BankAccountType")]
        private readonly BankAccountType bankAccountType;

        public string AccountNumber { get { return accountNumber; } }
        public string BranchCode { get { return branchCode; } }
        public BankAccountType BankAccountType { get { return bankAccountType; } }

        public BankAccount(string accountNumber, string branchCode, BankAccountType bankAccountType)
        {
            string cleanAccountNumber = CleanNumericString(accountNumber);
            string cleanBranchCode = CleanNumericString(branchCode);

            if (String.IsNullOrWhiteSpace(cleanAccountNumber) || String.IsNullOrWhiteSpace(cleanBranchCode) || bankAccountType == BankAccountType.Unknown)
            {
                this.accountNumber = String.Empty;
                this.branchCode = String.Empty;
                this.bankAccountType = BankAccountType.Unknown;
            }
            else
            {
                this.accountNumber = cleanAccountNumber;
                this.branchCode = cleanBranchCode;
                this.bankAccountType = bankAccountType;    
            }
        }

        private static string CleanNumericString(string s)
        {
            if (s == null)
                return String.Empty;

            return s.GetAllDigits();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (AccountNumber != null ? AccountNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BranchCode != null ? BranchCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ BankAccountType.GetHashCode();
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is BankAccount && Equals((BankAccount)obj);
        }

        public bool Equals(BankAccount other)
        {
            return String.Equals(AccountNumber, other.AccountNumber) 
                   && String.Equals(BranchCode, other.BranchCode) 
                   && BankAccountType.Equals(other.BankAccountType);
        }

        public override string ToString()
        {
            return String.Format("Account: {0} Branch: {1} Type: {2}", accountNumber, branchCode, bankAccountType);
        }
    }
}
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ValueObjects.People
{
    [DataContract(Name = "PersonAge", Namespace = "People")]
    public struct PersonAge : IEquatable<PersonAge>
    {
        [DataMember(Name = "age")]
        private readonly int age;

        public static PersonAge Empty { get { return new PersonAge(); } }

        public PersonAge(int age)
        {
            Mandate.That<PersonAgeException>(age >= 0, "Age cannot be a negative value.");

            this.age = age;
        }

        public static PersonAge AgeAtDate(DateTime dateOfBirth, DateTime specificDate)
        {
            Mandate.ParameterNotDefaut(dateOfBirth, "dateOfBirth");
            Mandate.ParameterNotDefaut(specificDate, "specificDate");

            int age = specificDate.Year - dateOfBirth.Year;

            if(age <= 0)
                return new PersonAge(age);

            if (dateOfBirth > specificDate.AddYears(-age))
            {
                age--;
            }

            if (specificDate.Month == DateTime.Now.Month && dateOfBirth.Day == DateTime.Now.Day)
            {
                age++;
            }
            
            if (specificDate.Month < DateTime.Now.Month)
            {
                age++;
            }

            return new PersonAge(age);
        }       

        public static implicit operator int(PersonAge age)
        {
            return age.age;
        }

        public override int GetHashCode()
        {
            return age;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is PersonAge && Equals((PersonAge)obj);
        }

        public bool Equals(PersonAge other)
        {
            return other.age == age;
        }

        public static bool operator ==(PersonAge left, PersonAge right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonAge left, PersonAge right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return age.ToString(CultureInfo.InvariantCulture);
        }
    }
}
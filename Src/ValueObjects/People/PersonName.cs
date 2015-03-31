using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace ValueObjects.People
{
    [DataContract(Name = "PersonName", Namespace = "People")]
    public struct PersonName : IEquatable<PersonName>
    {
        [DataMember(Name = "firstNames")]
        private readonly string firstNames;

        [DataMember(Name = "lastName")] 
        private readonly string lastName;

        [DataMember(Name = "initials")]
        private readonly string initials;

        [DataMember(Name = "title")] 
        private readonly Title title;        

        public string FirstName { get { return firstNames.Split(' ').First(); } }
        public string LastName { get { return lastName; } }
        public string Title { get { return title.ToString(); } }
        public string FormalName { get { return String.Format("{0}, {1}", lastName, firstNames); } }
        public string FullName { get { return ToString(); } }
        public string CorrespondenceName { get { return String.Join(" ", new[] {title.ToString(), initials, lastName}).Trim(); } }

        public static PersonName Empty { get { return new PersonName(); } }

        public PersonName(string firstName, string lastName)
            : this(People.Title.None, firstName, lastName)
        {
        }

        public PersonName(Title title, string firstName, string lastName)
        {
            Mandate.ParameterNotNullOrEmpty(firstName, "firstName");
            Mandate.ParameterNotNullOrEmpty(lastName, "lastName");

            this.firstNames = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(firstName.Trim());
            this.lastName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(lastName.Trim());
            this.title = title;
            initials = GetInitials(this.firstNames);
        }

        private static string GetInitials(string firstName)
        {
            char[] intialCharacters = firstName
                .ToUpperInvariant()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s[0])
                .ToArray();

            return new string(intialCharacters);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = firstNames.GetHashCode();
                result = (result * 397) ^ lastName.GetHashCode();
                result = (result * 397) ^ initials.GetHashCode();
                result = (result * 397) ^ title.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is PersonName && Equals((PersonName)obj);
        }

        public bool Equals(PersonName other)
        {
            return other.FirstName == FirstName
                   && other.LastName == LastName
                   && other.title == title
                   && other.initials == initials;
        }

        public static bool operator ==(PersonName left, PersonName right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonName left, PersonName right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return String.Join(" ", new[] {title.ToString(), firstNames, lastName}).Trim();
        }        
    }
}
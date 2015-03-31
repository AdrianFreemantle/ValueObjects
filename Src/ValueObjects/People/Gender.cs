using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ValueObjects.People
{
    [DataContract(Name = "Gender", Namespace = "People")]
    public struct Gender : IEquatable<Gender>, IEnumerable<Gender>
    {
        enum GenerTypes
        {
            [Description("")]
            Unknown = 0,
            Male = 1,
            Female = 2,
        }
        
        [DataMember(Name = "gender")]
        private readonly GenerTypes gender;

        public static Gender Unknown { get { return new Gender(GenerTypes.Unknown); } }
        public static Gender Male { get { return new Gender(GenerTypes.Male); } }
        public static Gender Female { get { return new Gender(GenerTypes.Female); } }

        public Gender(int gender)
        {
            this.gender = (GenerTypes)gender;
        }

        private Gender(GenerTypes gender)
        {
            this.gender = gender;
        }

        public Gender(string gender)
        {
            this.gender = DetermineGenderFromString(gender);
        }

        private static GenerTypes DetermineGenderFromString(string gender)
        {
            if (String.IsNullOrEmpty(gender))
                return GenerTypes.Unknown;

            if (gender.StartsWith("M", StringComparison.InvariantCultureIgnoreCase))
                return GenerTypes.Male;

            if (gender.StartsWith("F", StringComparison.InvariantCultureIgnoreCase))
                return GenerTypes.Female;

            return GenerTypes.Unknown;
        }

        public override int GetHashCode()
        {
            return (int)gender;
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

            return obj is Gender && Equals((Gender)obj);
        }

        public bool Equals(Gender other)
        {
            return other.gender == gender;
        }

        public static bool operator ==(Gender left, Gender right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Gender left, Gender right)
        {
            return !Equals(left, right);
        }

        public static implicit operator int(Gender gender)
        {
            return (int)gender.gender;
        }

        public IEnumerator<Gender> GetEnumerator()
        {
            return ObjectFactory.CreateInstances<Gender, GenerTypes>().GetEnumerator();
        }

        public override string ToString()
        {
            return gender.GetDescription();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.CSharp.RuntimeBinder;

namespace ValueObjects.People
{   
    [DataContract(Name = "Title", Namespace = "People")]
    public struct Title : IEquatable<Title>, IEnumerable<Title>
    {
        enum Titles
        {
            [Description("")]
            Unknown = 0,
            Mr = 1,
            Mrs = 2,
            Miss = 3,
            Dr = 4,
            Prof = 5,
            Hon = 6,
            Rev = 7,
            Ms = 8,
            Chief = 9
        }

        [DataMember(Name = "title")]
        private readonly Titles title;

        public static readonly Title None = new Title(Titles.Unknown);
        public static readonly Title Mr = new Title(Titles.Mr);
        public static readonly Title Mrs = new Title(Titles.Mrs);
        public static readonly Title Miss = new Title(Titles.Miss);
        public static readonly Title Dr = new Title(Titles.Dr);
        public static readonly Title Prof = new Title(Titles.Prof);
        public static readonly Title Hon = new Title(Titles.Hon);
        public static readonly Title Rev = new Title(Titles.Rev);
        public static readonly Title Ms = new Title(Titles.Ms);
        public static readonly Title Chief = new Title(Titles.Chief);

        public Title(int titleType)
        {
            this.title = (Titles)titleType;
        }

        private Title(Titles title)
        {
            this.title = title;
        }
        
        public Title(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                this.title = Titles.Unknown;
                return;
            }

            try
            {
                this.title = (Titles)Enum.Parse(typeof(Titles), title.Trim('.'));
                this.title.GetDescription(); //catch int value outside the allowed range
            }
            catch (Exception)
            {
                this.title = Titles.Unknown;
            }
        }

        public override int GetHashCode()
        {
            return (int)title;
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

            return obj is Title && Equals((Title)obj);
        }

        public bool Equals(Title other)
        {
            return other.title == title;
        }

        public static bool operator ==(Title left, Title right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Title left, Title right)
        {
            return !Equals(left, right);
        }

        public static implicit operator int(Title title)
        {
            return (int)title.title;
        }

        public IEnumerator<Title> GetEnumerator()
        {
            return ObjectFactory.CreateInstances<Title, Titles>().GetEnumerator();
        }

        public override string ToString()
        {
            return title.GetDescription();
        }
    }
}
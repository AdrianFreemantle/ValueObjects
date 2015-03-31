using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.People;

namespace ValueObjects.Tests
{
    [TestClass]
    public class PersonNameTests
    {
        static readonly PersonName NameWithTitle = new PersonName(People.Title.Mr, "Peter Mark", "Smith");
        static readonly PersonName NameWithoutTitle = new PersonName("Peter Mark", "Smith");

        [TestMethod]
        public void Full_name()
        {
            NameWithTitle.FullName.ShouldBe("Mr Peter Mark Smith");
            NameWithoutTitle.FullName.ShouldBe("Peter Mark Smith");
        }

        [TestMethod]
        public void Title()
        {
            NameWithTitle.Title.ShouldBe("Mr");
            NameWithoutTitle.Title.ShouldBe("");
        }

        [TestMethod]
        public void First_name()
        {
            NameWithTitle.FirstName.ShouldBe("Peter");
            NameWithoutTitle.FirstName.ShouldBe("Peter");
        }

        [TestMethod]
        public void Last_name()
        {
            NameWithTitle.LastName.ShouldBe("Smith");
            NameWithoutTitle.LastName.ShouldBe("Smith");
        }

        [TestMethod]
        public void Formal_name()
        {
            NameWithTitle.FormalName.ShouldBe("Smith, Peter Mark");
            NameWithoutTitle.FormalName.ShouldBe("Smith, Peter Mark");
        }

        [TestMethod]
        public void Correspondence_name()
        {
            NameWithTitle.CorrespondenceName.ShouldBe("Mr PM Smith");
            NameWithoutTitle.CorrespondenceName.ShouldBe("PM Smith");
        }
    }
}

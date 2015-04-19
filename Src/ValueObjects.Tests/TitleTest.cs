using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using ValueObjects.People;

namespace ValueObjects.Tests
{
    [TestClass]
    public class TitleTest
    {
        [TestMethod]
        public void Miss_can_be_parsed()
        {
            new Title("MISS").ShouldBe(Title.Miss);
        }

        [TestMethod]
        public void Dot_notation_can_be_parsed()
        {
            new Title("Miss.").ShouldBe(Title.Miss);
        }

        [TestMethod]
        public void Extra_whitespace_can_be_parsed()
        {
            new Title(" Miss ").ShouldBe(Title.Miss);
        }

        [TestMethod]
        public void Incorrect_title_returns_empty_string()
        {
            new Title("BLERT").ToString().ShouldBe(String.Empty);
        }
    }
}
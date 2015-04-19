using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ValueObjects.Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void Can_replace_first_occurece_of_character()
        {
            "peter.gmail.com".ReplaceFirstInstanceOf('.', '@').ShouldBe("peter@gmail.com");
        }

        [TestMethod]
        public void Can_replace_last_occurece_of_character()
        {
            "peter.smith2gmail.com".ReplaceLastInstanceOf('2', '@').ShouldBe("peter.smith@gmail.com");
        }
    }
}
using System;
using NUnit.Framework;

namespace ArchitectureLibrary.Tests
{
    public class StringFormatterTests
    {
        #region ToTitleCase
        private void ToTitleCase_Test_Positive(string value, string expected)
        {
            // Assign
            // Act
            string result = value.ToTitleCase();

            // Assert
            Assert.AreEqual(expected, result);
        }
        private void ToTitleCase_Test_Negative(string value, string expected)
        {
            // Assign
            // Act
            string result = value.ToTitleCase();

            // Assert
            Assert.AreNotEqual(expected, result);
        }
        [Test]
        public void ToTitleCase__Value_CamelCaseString__Return_TitleCaseString() =>
            ToTitleCase_Test_Positive("helloWorld", "Hello World");
        [Test]
        public void ToTitleCase__Value_PascalCaseString__Return_TitleCaseString() =>
            ToTitleCase_Test_Positive("HelloWorld", "Hello World");
        [Test]
        public void ToTitleCase__Value_SnakeCaseString__Return_TitleCaseString() =>
            ToTitleCase_Test_Positive("hello_world", "Hello World");
        [Test]
        public void ToTitleCase__Value_TitleCaseString__Return_TitleCaseString() =>
            ToTitleCase_Test_Positive("Hello World", "Hello World");
        [Test]
        public void ToTitleCase__Value_TitleCaseStringWithManySpaces__Return_TitleCaseString() =>
            ToTitleCase_Test_Positive(" Hello  World   ", "Hello World");
        [Test]
        public void ToTitleCase__Value_MixedCaseString__Return_TitleCaseString() =>
            ToTitleCase_Test_Positive("  helloWorld How_areYou _I'mWell_Thanks ", "Hello World How Are You I'm Well Thanks");
        #endregion

        #region CapitalizeFirst
        [Test]
        public void CapitalizeFirst__Value_EmptyString__Return_EmptyString() =>
            CapitalizeFirst_Test_Positive(string.Empty, string.Empty);
        [Test]
        public void CapitalizeFirst__Value_SingleCharString__Return_UpperCaseString() =>
            CapitalizeFirst_Test_Positive("a", "A");
        [Test]
        public void CapitalizeFirst__Value_MultiCharString__Return_CapitalizedString() =>
            CapitalizeFirst_Test_Positive("hello world", "Hello world");
        [Test]
        public void CapitalizeFirst__Value_WhitespaceString__Return_WhitespaceString() =>
            CapitalizeFirst_Test_Positive("   ", "   ");
        [Test]
        public void CapitalizeFirst__Value_Null__Return_EmptyString() =>
            CapitalizeFirst_Test_Positive(null, "");
        [Test]
        public void CapitalizeFirst__Value_LowerCaseString__NotReturn_LowerCaseString() =>
            CapitalizeFirst_Test_Negative("hello world", "hello world");
        private void CapitalizeFirst_Test_Positive(string value, string expected)
        {
            // Assign
            // Act
            string result = value.CapitalizeFirst();

            // Assert
            Assert.AreEqual(expected, result);
        }
        private void CapitalizeFirst_Test_Negative(string value, string expected)
        {
            // Assign
            // Act
            string result = value.CapitalizeFirst();

            // Assert
            Assert.AreNotEqual(expected, result);
        }
        #endregion
    }
}
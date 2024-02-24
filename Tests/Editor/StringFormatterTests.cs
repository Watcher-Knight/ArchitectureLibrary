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
    }
}
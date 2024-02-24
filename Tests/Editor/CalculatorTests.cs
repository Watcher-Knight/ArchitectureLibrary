using NUnit.Framework;
using UnityEngine;

namespace ArchitectureLibrary.Tests
{
    public class CalculatorTests
    {
        private void ToDirection_Test_Positive(float rotation, Vector2 expected)
        {
            // Assign
            // Act
            Vector2 result = Calculator.ToDirection(rotation);

            // Assert
            Assert.IsTrue(expected == result, $"Expected: {expected}, Result: {result}");
        }
        private void ToDirection_Test_Negative(float rotation, Vector2 expected)
        {
            // Assign
            // Act
            Vector2 result = Calculator.ToDirection(rotation);

            // Assert
            Assert.IsFalse(expected == result, $"Expected: {expected}, Result: {result}");
        }
        [Test]
        public void ToDirection__Value_0__Return_x0y1() =>
            ToDirection_Test_Positive(0, new(0, 1));
        [Test]
        public void ToDirection__Value_180__Return_x0yNeg1() =>
            ToDirection_Test_Positive(180, new(0, -1));
        [Test]
        public void ToDirection__Value_90__Return_x1y0() =>
            ToDirection_Test_Positive(90, new(1, 0));
        [Test]
        public void ToDirection__Value_Neg90__Return_xNeg1y0() =>
            ToDirection_Test_Positive(-90, new(-1, 0));
        [Test]
        public void ToDirection__Value_30__NotReturn_x1Dec3y0Dec7() =>
            ToDirection_Test_Negative(30, new(1.3f, 0.7f));

        private void ToRotation_Test_Positive(Vector2 direction, float expected)
        {
            // Assign
            // Act
            float result = Calculator.ToRotation(direction);

            // Assert
            Assert.AreEqual(expected, result);
        }
        private void ToRotation_Test_Negative(Vector2 direction, float expected)
        {
            // Assign
            // Act
            float result = Calculator.ToRotation(direction);

            // Assert
            Assert.AreNotEqual(expected, result);
        }
        [Test]
        public void ToRotation__Value_x0y1__Return_0() =>
            ToRotation_Test_Positive(new(0, 1), 0);
        [Test]
        public void ToRotation__Value_x0yNeg1_Return_180() =>
            ToRotation_Test_Positive(new(0, -1), 180);
        [Test]
        public void ToRotation__Value_x1y0__Return_90() =>
            ToRotation_Test_Positive(new(1, 0), 90);
        [Test]
        public void ToRotation__Value_xNeg1y0__Return_90() =>
            ToRotation_Test_Positive(new(-1, 0), -90);
        [Test]
        public void ToRotation__Value_zero__Return_0() =>
            ToRotation_Test_Positive(Vector2.zero, 0);
            [Test]
        public void ToRotation__Value_x9Dec3y7Dec8__NotReturn_276() =>
            ToRotation_Test_Negative(new(9.3f, 7.8f), 276);

        private void GetDistance_Test_Positive(Vector2 p1, Vector2 p2, float expected)
        {
            // Assign
            // Act
            float result = Calculator.GetDistance(p1, p2);

            // Assert
            Assert.AreEqual(expected, result);
        }
        private void GetDistance_Test_Negative(Vector2 p1, Vector2 p2, float expected)
        {
            // Assert
            // Act
            float result = Calculator.GetDistance(p1, p2);

            // Assert
            Assert.AreNotEqual(expected, result);
        }
        [Test]
        public void GetDistance__Values_zero_x0y3__Return_3() =>
        GetDistance_Test_Positive(Vector2.zero, new(0, 3), 3);
        [Test]
        public void GetDistance__Values_zero_x3y0__Return_3() =>
        GetDistance_Test_Positive(Vector2.zero, new(3, 0), 3);
        [Test]
        public void GetDistance__Values_zero_x3y4__Return_5() =>
            GetDistance_Test_Positive(Vector2.zero, new(3, 4), 5);
        [Test]
        public void GetDistance__Values_x12y5_zero__Return_13() =>
            GetDistance_Test_Positive(new(12, 5), Vector2.zero, 13);
        [Test]
        public void GetDistance__Values_x15y6_x22y30__Return_25() =>
            GetDistance_Test_Positive(new(15, 6), new(22, 30), 25);
        [Test]
        public void GetDistance__Values_x3y7_x58y12__NotReturn_25() =>
            GetDistance_Test_Negative(new(3, 7), new(58, 12), 25);
    }
}
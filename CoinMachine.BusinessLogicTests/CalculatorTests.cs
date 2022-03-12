using CoinMachine.BusinessLogic;
using CoinMachine.Core.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CoinMachine.BusinessLogicTests
{
    [TestClass]
    public class CalculatorTests
    {
        #region Tests not defined in spec

        [TestMethod]
        public void Count_WithTestCurrency_ExpectedReturned()
        {
            // Arrange
            var amountDeposited = 4; // 4p
            var denominations = new int[] { 3, 2, 1 };

            var calculator = new Calculator<TestCurrency>();

            // Act
            var result = calculator.Count(denominations, denominations.Length, amountDeposited, 0);

            // Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void GetSolutionsCount_WithTestCurrency_ExpectedReturned()
        {
            // Arrange
            var amountDeposited = 0.04m; // 4p
            var calculator = new Calculator<TestCurrency>();

            // Act
            var result = calculator.GetSolutionsCount(amountDeposited);

            // Assert
            Assert.AreEqual(4, result.SolutionsCount);
        }

        [TestMethod]
        public void GetSolutionsCount_WithTestCurrency2_ExpectedReturned()
        {
            // Arrange
            var amountDeposited = 0.1m; // 10p
            var calculator = new Calculator<TestCurrency2>();

            // Act
            var result = calculator.GetSolutionsCount(amountDeposited);

            // Assert
            //{2,2,2,2,2}, {2,2,3,3}, {2,2,6}, {2,3,5} and {5,5}
            Assert.AreEqual(5, result.SolutionsCount);
        }

        [TestMethod]
        public void GetSolutionsOddCount_WithTestCurrency_ExpectedReturned()
        {
            // Arrange
            var amountDeposited = 0.04m; // 4p
            var calculator = new Calculator<TestCurrency>();

            // Act
            var result = calculator.GetSolutionsOddCount(amountDeposited);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetSolutionsOddCount_WithTestCurrency2_ExpectedReturned()
        {
            // Arrange
            var amountDeposited = 0.1m; // 10p
            var calculator = new Calculator<TestCurrency2>();

            // Act
            var result = calculator.GetSolutionsOddCount(amountDeposited);

            // Assert
            //{2,2,2,2,2}, {2,2,3,3}, {2,2,6}, {2,3,5} and {5,5}
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        [DataRow(0.5, 4)]
        [DataRow(2, 41)]
        public void GetSolutionsCount_WithSterlingCurrency_ExpectedReturned(double amountDeposited, int expected)
        {
            // Arrange
            decimal amountDepositedAsDecimal = Convert.ToDecimal(amountDeposited);
            var calculator = new Calculator<PoundSterling>();

            // Act
            var result = calculator.GetSolutionsCount(amountDepositedAsDecimal);

            // Assert
            Assert.AreEqual(expected, result.SolutionsCount);
        }

        #endregion

        #region Tests from Spec

        [TestMethod]
        [DataRow(0.5, 3)] // 50p
        [DataRow(2, 20)] // £2
        [DataRow(10, 2054)] // £10
        [DataRow(20, 23845)] // £20
        //[DataRow(50, 755144)] // £50 3.8 seconds
        //[DataRow(100, 11228725)] // 1.7 minutes
        public void GetSolutionsOddCount_WithPoundSterling_ExpectedReturned(double amountDeposited, int expected)
        {
            // Arrange
            var calculator = new Calculator<PoundSterling>();

            decimal amountDepositedAsDecimal = Convert.ToDecimal(amountDeposited);

            // Act
            var result = calculator.GetSolutionsOddCount(amountDepositedAsDecimal);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("£0-5", 3)]
        [DataRow("£2-", 20)]
        [DataRow("£10-", 2054)]
        [DataRow("£20-", 23845)]
        //[DataRow("£50-", 755144)] // 3.8 second
        //[DataRow("£100-", 11228725)] // 1.7 minutes
        public void GetChangeOddCoinCount_WithPoundSterling_ExpectedReturned(string input, int expected)
        {
            // Arrange
            var calculator = new Calculator<PoundSterling>();

            // Act
            var result = calculator.GetChangeOddCoinCount(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}

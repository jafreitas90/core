using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineCalculator.UnitTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            int a = 1;
            int b = 2;
            var calculator = new Calculator();

            // Act
            var result = calculator.Sum(a, b);

            // Assert
            Assert.AreEqual(a + b, result);
        }
    }
}

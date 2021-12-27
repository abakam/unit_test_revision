using dotnetcore_web_api.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dotnetcore_web_api.Test
{
    public class CalculatorControllerTest
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorControllerTest()
        {
            _calculatorService = new CalculatorService();
        }

        [Fact]
        public void Add()
        {
            //Arrange
            double a = 5;
            double b = 3;
            double expected = 8;

            //Act
            var actual = _calculatorService.Add(a, b);

            //Assert
            Assert.Equal(expected, actual, 0);
        }

        [Fact]
        public void Substract()
        {
            //Arrange
            double x1 = 10;
            double x2 = 8;
            double expected = 2;

            //Act
            var actual = _calculatorService.Subtract(x1, x2);

            //Assert
            Assert.Equal(expected, actual, 0);
        }

        [Theory(DisplayName = "Maths - Divided with parameters")]
        [InlineData(40, 8, 5)]
        public void Divide(double value1, double value2, double value3)
        {
            //Arrange
            double x1 = value1;
            double x2 = value2;
            double expected = value3;

            //Act
            var actual = _calculatorService.Divide(x1, x2);

            //Assert
            Assert.Equal(expected, actual, 0);
        }

        [Fact(Skip = "Do not run now")]
        public void Multiply()
        {
            //Arrange
            double x1 = 5;
            double x2 = 8;
            double expected = 40;

            //Act
            var actual = _calculatorService.Multiply(x1, x2);

            //Assert
            Assert.Equal(expected, actual, 0);
        }

        [Fact(DisplayName = "Maths - Divide by Zero Exception")]
        public void DivideByZeroException()
        {
            //Arrange
            double a = 100;
            double b = 0;

            //Act
            Action act = () => _calculatorService.Divide(a, b);

            //Assert
            Assert.Throws<DivideByZeroException>(act);
        }
    }
}

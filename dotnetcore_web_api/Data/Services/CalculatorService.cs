using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore_web_api.Data.Services
{
    public class CalculatorService : ICalculatorService
    {
        public double Add(double x1, double x2) => x1 + x2;

        public double Divide(double x1, double x2)
        {
            if (x2 == 0)
                throw new DivideByZeroException("x2 cannot be zero");

            return (x1 / x2);
        }

        public double Multiply(double x1, double x2) => x1 * x2;

        public double Subtract(double x1, double x2) => x1 - x2;
    }
}

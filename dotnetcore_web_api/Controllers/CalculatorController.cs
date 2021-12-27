using dotnetcore_web_api.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost]
        public double Add(double x1, double x2)
        {
            return _calculatorService.Add(x1, x2);
        }

        [HttpPost]
        public double Divide(double x1, double x2)
        {
            return _calculatorService.Divide(x1, x2);
        }

        [HttpPost]
        public double Multiply(double x1, double x2)
        {
            return _calculatorService.Multiply(x1, x2);
        }

        [HttpPost]
        public double Subtract(double x1, double x2)
        {
            return _calculatorService.Subtract(x1, x2);
        }
    }
}

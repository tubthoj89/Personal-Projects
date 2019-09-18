using Calculator.Interface;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Repository
{
    public class CalculationRepositoryMock : ICalculationRepository
    {
        private static List<Calculation> _calculation;

        static CalculationRepositoryMock()
        {
            _calculation = new List<Calculation>()
            {
                new Calculation { CalculationId = 1, MathCal = "10 * 5 = 50" },
                new Calculation { CalculationId = 2, MathCal = "2 + 15 = 17" },
                new Calculation { CalculationId = 3, MathCal = "20 * 5 = 100" },
                new Calculation { CalculationId = 4, MathCal = "215 - 150 = 65" },
                new Calculation { CalculationId = 5, MathCal = "500 / 5 = 100" }
            };
        }

        public void CreateCalculation(Calculation calculation)
        {
            if (_calculation.Any())
            {
                calculation.CalculationId = _calculation.Max(c => c.CalculationId) + 1;
                _calculation.Add(calculation);
                if (_calculation.Count() >= 11)
                {
                    _calculation.RemoveAt(0);
                }
            }
            else
            {
                calculation.CalculationId = 1;
                _calculation.Add(calculation);
            }            
        }

        public IEnumerable<Calculation> GettAllCalculations()
        {
            return _calculation.OrderByDescending(c => c.CalculationId);
        }
    }
}
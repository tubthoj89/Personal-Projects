using Calculator.Interface;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Repository
{
    public class CalculationRepository : ICalculationRepository
    {
        public void CreateCalculation(Calculation calculation)
        {
            var repo = new CalculatorEntities();

            if (repo.Calculations.Any())
            {
                calculation.CalculationId = repo.Calculations.Max(c => c.CalculationId) + 1;
                repo.Calculations.Add(calculation);

                repo.SaveChanges();
            }
            else
            {
                calculation.CalculationId = 1;
                repo.Calculations.Add(calculation);
                repo.SaveChanges();
            }
        }

        public IEnumerable<Calculation> GettAllCalculations()
        {
            var repo = new CalculatorEntities();

            var cal = repo.Calculations.OrderByDescending(c => c.CalculationId).Take(10);

            return cal;
        }
    }
}
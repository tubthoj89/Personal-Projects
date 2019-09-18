using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Models
{
    public class CalculationVM
    {
        public Calculation Calculation { get; set; }
        public IEnumerable<Calculation> Calculations { get; set; }
    }
}
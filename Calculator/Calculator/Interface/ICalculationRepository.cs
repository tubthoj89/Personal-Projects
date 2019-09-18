﻿using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Interface
{
    public interface ICalculationRepository
    {
        IEnumerable<Calculation> GettAllCalculations();
        void CreateCalculation(Calculation calculation);
    }
}

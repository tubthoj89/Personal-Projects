using Calculator.Interface;
using Calculator.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Factory
{
    public class CalculatorFactory
    {
        public static ICalculationRepository GetRepository()
        {
            switch (FactoryHelper.GetRepositoryType())
            {
                case "Mock":
                    return new CalculationRepositoryMock();
                case "Production":
                    return new CalculationRepository();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
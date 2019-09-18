using Calculator.Models;
using Calculator.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Test
{
    [TestFixture]
    public class UnitTesting
    {
        [Test]
        public void CanGetAllCalculation()
        {
            CalculationRepositoryMock _calRepo = new CalculationRepositoryMock();

            List<Calculation> calculations = _calRepo.GettAllCalculations().ToList();

            Assert.AreEqual(6, calculations.Count);

            Calculation check = calculations.FirstOrDefault(c => c.CalculationId == 1);

            Assert.AreEqual(1, check.CalculationId);
            Assert.AreEqual("10 * 5 = 50", check.MathCal);
        }

        [Test]
        public void GetAllCalculationWrong()
        {
            CalculationRepositoryMock _calRepo = new CalculationRepositoryMock();

            List<Calculation> calculations = _calRepo.GettAllCalculations().ToList();

            Assert.AreNotEqual(7, calculations.Count);

            Calculation check = calculations.FirstOrDefault(c => c.CalculationId == 1);

            Assert.AreNotEqual(2, check.CalculationId);
            Assert.AreNotEqual("12 / 3 = 4", check.MathCal);
        }

        [Test]
        public void CanAddCalculation()
        {
            CalculationRepositoryMock _calRepo = new CalculationRepositoryMock();

            Calculation calculation = new Calculation()
            {
                CalculationId = 6,
                MathCal = "12 / 3 = 4"
            };
            _calRepo.CreateCalculation(calculation);

            List<Calculation> calculations = _calRepo.GettAllCalculations().ToList();

            Assert.AreEqual(6, calculations.Count);

            Calculation check = calculations.FirstOrDefault(c => c.CalculationId == 3);

            Assert.AreEqual(3, check.CalculationId);
            Assert.AreEqual("20 * 5 = 100", check.MathCal);
        }
    }
}
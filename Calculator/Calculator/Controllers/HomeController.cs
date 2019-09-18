using Calculator.Factory;
using Calculator.Interface;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        ICalculationRepository _calRepo = CalculatorFactory.GetRepository();

        public ActionResult Index()
        {
            //CalculationVM model = new CalculationVM();
            //model.Calculations = _calRepo.GettAllCalculations();
            return View(/*model*/);
        }

        [HttpPost]
        public ActionResult AddCalculation(Calculation calculation)
        {
            try
            {
                double[] results = Regex.Split(calculation.MathCal, @"-|\+|\*|\/").Select(x => Convert.ToDouble(x)).ToArray();

                //if ((calculation.MathCal.Trim()).StartsWith(".")[1])
                //    textBox1.Text = "0" + textBox1.Text;

                string operation = Regex.Split(calculation.MathCal, @"[0-9]+")[1];

                switch (operation)
                {
                    case "+.":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] + results[1]}";
                        break;
                    case "-.":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] - results[1]}";
                        break;
                    case "*.":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] * results[1]}";
                        break;
                    case "/.":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] / results[1]}";
                        break;
                    case "+":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] + results[1]}";
                        break;
                    case "-":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] - results[1]}";
                        break;
                    case "*":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] * results[1]}";
                        break;
                    case "/":
                        calculation.MathCal = $"{results[0]} {operation} {results[1]} = {results[0] / results[1]}";
                        break;
                    default:
                        break;
                }

                _calRepo.CreateCalculation(calculation);
            }
            catch (Exception)
            {
                calculation.MathCal = "NaN";
                _calRepo.CreateCalculation(calculation);
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
    }
}
//using Microsoft.AspNetCore.Mvc;
//using System;

//namespace lab8.Controllers
//{
//    public class ToolController : Controller
//    {
//        [Route("Tool/Solve/{a}/{b}/{c}")]
//        public IActionResult Solve(double a, double b, double c)
//        {
//            string message;
//            string cssClass;

//            if (a == 0 && b == 0 && c == 0)
//            {
//                message = "Równanie tożsamościowe – nieskończenie wiele rozwiązań.";
//                cssClass = "infinite";
//            }
//            else if (a == 0 && b == 0)
//            {
//                message = "Brak rozwiązań.";
//                cssClass = "none";
//            }
//            else if (a == 0)
//            {
//                // równanie liniowe
//                double x = -c / b;
//                message = $"Równanie liniowe. Rozwiązanie: x = {x}";
//                cssClass = "one";
//            }
//            else
//            {
//                // równanie kwadratowe
//                double delta = b * b - 4 * a * c;

//                if (delta > 0)
//                {
//                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
//                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
//                    message = $"Dwa rozwiązania: x₁ = {x1}, x₂ = {x2}";
//                    cssClass = "two";
//                }
//                else if (delta == 0)
//                {
//                    double x = -b / (2 * a);
//                    message = $"Jedno rozwiązanie: x = {x}";
//                    cssClass = "one";
//                }
//                else
//                {
//                    message = "Brak rozwiązań rzeczywistych (Δ < 0).";
//                    cssClass = "none";
//                }
//            }

//            ViewBag.Message = message;
//            ViewBag.CssClass = cssClass;

//            return View();
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; 

namespace lab8.Controllers
{
    public class SolutionResult
    {
        public int Count { get; set; } // Liczba rozwiązań (0, 1, 2, lub -1 dla tożsamościowego)
        public List<double> Solutions { get; set; } = new List<double>();
    }

    public class ToolController : Controller
    {
        [Route("Tool/Solve/{a}/{b}/{c}")]
        public IActionResult Solve(double a, double b, double c)
        {
            SolutionResult result = SolveQuadraticEquation(a, b, c);

            SetViewData(a, b, c, result);

            return View();
        }

        // ---------------------------------
        private SolutionResult SolveQuadraticEquation(double a, double b, double c)
        {
            var result = new SolutionResult();

            if (a == 0 && b == 0 && c == 0)
            {
                result.Count = -1;
            }
            else if (a == 0 && b == 0)
            {
                result.Count = 0;
            }
            else if (a == 0)
            {
                result.Solutions.Add(-c / b);
                result.Count = 1;
            }
            else
            {
                double delta = b * b - 4 * a * c;

                if (delta > 0)
                {
                    result.Solutions.Add((-b + Math.Sqrt(delta)) / (2 * a));
                    result.Solutions.Add((-b - Math.Sqrt(delta)) / (2 * a));
                    result.Count = 2;
                }
                else if (delta == 0)
                {
                    result.Solutions.Add(-b / (2 * a));
                    result.Count = 1;
                }
                else
                {
                    result.Count = 0;
                }
            }

            return result;
        }

        // ------------------------
        private void SetViewData(double a, double b, double c, SolutionResult result)
        {
            string message;
            string cssClass;

            string equation = $"{a}x² + {b}x + {c} = 0";

            if (result.Count == -1)
            {
                message = $"Równanie tożsamościowe: {equation} – nieskończenie wiele rozwiązań.";
                cssClass = "infinite";
            }
            else if (result.Count == 2)
            {
                double x1 = result.Solutions[0];
                double x2 = result.Solutions[1];
                message = $"Dwa rozwiązania dla {equation}: x₁ = {x1}, x₂ = {x2}";
                cssClass = "two";
            }
            else if (result.Count == 1)
            {
                double x = result.Solutions[0];
                string type = (a == 0) ? "liniowe" : "kwadratowe";
                message = $"Jedno rozwiązanie ({type}) dla {equation}: x = {x}";
                cssClass = "one";
            }
            else
            {
                message = $"Brak rozwiązań rzeczywistych dla {equation}.";
                cssClass = "none";
            }

            ViewBag.Message = message;
            ViewBag.CssClass = cssClass;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;

namespace lab8.Controllers
{
    public class ToolController : Controller
    {
        [Route("Tool/Solve/{a}/{b}/{c}")]
        public IActionResult Solve(double a, double b, double c)
        {
            string message;
            string cssClass;

            if (a == 0 && b == 0 && c == 0)
            {
                message = "Równanie tożsamościowe – nieskończenie wiele rozwiązań.";
                cssClass = "infinite";
            }
            else if (a == 0 && b == 0)
            {
                message = "Brak rozwiązań.";
                cssClass = "none";
            }
            else if (a == 0)
            {
                // równanie liniowe
                double x = -c / b;
                message = $"Równanie liniowe. Rozwiązanie: x = {x}";
                cssClass = "one";
            }
            else
            {
                // równanie kwadratowe
                double delta = b * b - 4 * a * c;

                if (delta > 0)
                {
                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                    message = $"Dwa rozwiązania: x₁ = {x1}, x₂ = {x2}";
                    cssClass = "two";
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    message = $"Jedno rozwiązanie: x = {x}";
                    cssClass = "one";
                }
                else
                {
                    message = "Brak rozwiązań rzeczywistych (Δ < 0).";
                    cssClass = "none";
                }
            }

            ViewBag.Message = message;
            ViewBag.CssClass = cssClass;

            return View();
        }
    }
}

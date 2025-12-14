using Microsoft.AspNetCore.Mvc;
using System;

namespace lab8.Controllers
{
    public class GameController : Controller
    {
    
        private static int RangeN = 100;
        private static int RandValue;
        private static int AttemptsCount = 0;
        private static readonly Random Rnd = new Random();

        [Route("Set,{n}")]
        public IActionResult Set(int n)
        {
            if (n <= 0)
            {
                ViewBag.Message = $"Błąd: Zakres musi być liczbą całkowitą większą niż 0. Podano: {n}.";
                ViewBag.CssClass = "error-state";
                return View("GameStatus");
            }

            RangeN = n;
            AttemptsCount = 0;
            RandValue = Rnd.Next(0, RangeN);

            ViewBag.Message = $"Ustawiono nowy zakres losowania n={RangeN}. Wylosowano nową liczbę (0 do n-1).";
            ViewBag.CssClass = "set-state";
            return View("GameStatus");
        }

        [Route("Draw")]
        public IActionResult Draw()
        {
            RandValue = Rnd.Next(0, RangeN);
            AttemptsCount = 0;

            ViewBag.Message = $"Wylosowano nową liczbę w zakresie od 0 do {RangeN - 1}. Rozpocznij zgadywanie!";
            ViewBag.CssClass = "draw-state";
            return View("GameStatus");
        }

        [Route("Guess,{guess}")]
        public IActionResult Guess(int guess)
        {
            AttemptsCount++;

            ViewBag.Attempt = AttemptsCount;
            ViewBag.Range = RangeN;

            if (guess == RandValue)
            {
                ViewBag.Message = $"Brawo! Zgadłeś liczbę {RandValue} w {AttemptsCount} próbach.";
                ViewBag.CssClass = "win-state";
            }
            else if (guess < RandValue)
            {
                ViewBag.Message = $"{guess} - Za mała liczba. Próbuj dalej!";
                ViewBag.CssClass = "low-state";
            }
            else 
            {
                ViewBag.Message = $"{guess} - Za duża liczba. Próbuj dalej!";
                ViewBag.CssClass = "high-state";
            }

            return View("GameStatus");
        }
    }
}
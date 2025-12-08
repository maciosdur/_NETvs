using Microsoft.AspNetCore.Mvc;
using System;

namespace lab8.Controllers
{
    public class GameController : Controller
    {
        // --- ZMIENNE STATYCZNE PRZECHOWUJĄCE STAN GRY ---
        private static int RangeN = 100; // Domyślny zakres, jeśli Set nie zostanie wywołane
        private static int RandValue;
        private static int AttemptsCount = 0;
        private static readonly Random Rnd = new Random();

        // Akcja wywoływana przez /Set,N
        // Używamy atrybutu [Route] dla specyficznych wzorców
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
            AttemptsCount = 0; // Resetujemy próby
            // Losowanie nowej liczby od razu po ustawieniu zakresu jest dobrą praktyką
            RandValue = Rnd.Next(0, RangeN);

            ViewBag.Message = $"Ustawiono nowy zakres losowania $n={RangeN}$. Wylosowano nową liczbę (0 do $n-1$).";
            ViewBag.CssClass = "set-state";
            return View("GameStatus");
        }

        // Akcja wywoływana przez /Draw
        [Route("Draw")]
        public IActionResult Draw()
        {
            // Losowanie nowej liczby z aktualnie ustawionego zakresu (RangeN)
            RandValue = Rnd.Next(0, RangeN);
            AttemptsCount = 0; // Zerowanie licznika prób

            ViewBag.Message = $"Wylosowano nową liczbę w zakresie od 0 do {RangeN - 1}. Rozpocznij zgadywanie!";
            ViewBag.CssClass = "draw-state";
            return View("GameStatus");
        }

        // Akcja wywoływana przez /Guess,X
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
                // Można tu wylosować nową liczbę, aby przygotować następną grę
                // RandValue = Rnd.Next(0, RangeN);
                // AttemptsCount = 0;
            }
            else if (guess < RandValue)
            {
                ViewBag.Message = $"Za mała liczba. Próbuj dalej!";
                ViewBag.CssClass = "low-state";
            }
            else // guess > RandValue
            {
                ViewBag.Message = $"Za duża liczba. Próbuj dalej!";
                ViewBag.CssClass = "high-state";
            }

            return View("GameStatus");
        }
    }
}
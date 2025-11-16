using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Podaj a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Podaj b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Podaj c: ");
        double c = double.Parse(Console.ReadLine());

        var (code, solutions) = SolveQuadratic(a, b, c);

        if (code == -1)
        {
            Console.WriteLine("Równanie ma nieskończenie wiele rozwiązań.");
        }
        else if (code == 0)
        {
            Console.WriteLine("Brak rozwiązań rzeczywistych.");
        }
        else if (code == 1)
        {
            Console.WriteLine("Jedno rozwiązanie: x = {0:F4}", solutions[0]);
        }
        else if (code == 2)
        {
            Console.WriteLine($"Dwa rozwiązania: x1 = {solutions[0]:F4}, x2 = {solutions[1]:F4}");
        }
    }

    static (int code, double[] solutions) SolveQuadratic(double a, double b, double c)
    {
        // 1. Wszystko zero → nieskończenie wiele
        if (a == 0 && b == 0 && c == 0)
        {
            return (-1, Array.Empty<double>());
        }

        // 2. a = 0 → równanie liniowe
        if (a == 0)
        {
            if (b == 0)
                return (0, Array.Empty<double>()); // sprzeczne

            return (1, new double[] { -c / b });
        }

        // 3. Równanie kwadratowe
        double delta = b * b - 4 * a * c;

        if (delta < 0)
            return (0, Array.Empty<double>());
        else if (delta == 0)
            return (1, new double[] { -b / (2 * a) });
        else
        {
            double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
            double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
            return (2, new double[] { x1, x2 });
        }
    }
}
using System;

class Program
{
    static void Main(string[] args)
    {
        // 1.
        Console.WriteLine("Zadanie 1");
        var person = ("Maciej", "Durkalec", 20, 9999.50);
        ShowPersonInfo(person);

        // 2.
        Console.WriteLine("\nZadanie 2");
        string @class = "To jest wartość zapisania do zmiennej o nazwie class.";
        Console.WriteLine(@class);

        // 3.
        Console.WriteLine("\nZadanie 3");
        int[] numbers = { 5, 3, 9, 1, 4 };

        // 1. Array.Sort — sortowanie tablicy
        Array.Sort(numbers);
        Console.WriteLine("1. Posortowane: " + string.Join(", ", numbers));

        // 2. Array.Reverse — odwracanie kolejności
        Array.Reverse(numbers);
        Console.WriteLine("2. Odwrócone: " + string.Join(", ", numbers));

        // 3. Array.IndexOf — szukanie indeksu elementu
        int index = Array.IndexOf(numbers, 4);
        Console.WriteLine("3. Index elementu 4: " + index);

        // 4. Array.Exists — sprawdza czy jakiś element spełnia warunek
        bool exists = Array.Exists(numbers, x => x > 7);
        Console.WriteLine("4. Czy jest liczba > 7? " + exists);

        // 5. Array.Find — zwraca pierwszy element spełniający warunek
        int found = Array.Find(numbers, x => x < 4);
        Console.WriteLine("5. Pierwsza liczba < 4: " + found);

        // 4.
        Console.WriteLine("\nZadanie 4");
        var anon = new { Imie = "Kasia", Wiek = 21, Miasto = "Wrocław" };
        UseAnonymous(anon);

        // 5.
        Console.WriteLine("\nZadanie 5");
        DrawCard("Ryszssssssssssard", "Rys", 'X', 2, 20);
        Console.WriteLine();
        DrawCard("Maciej", "dur");
        Console.WriteLine();
        DrawCard(
            line1: "Politechnika Wrocławska",
            line2: "W4",
            borderChar: '#',
            borderWidth: 1,
            minWidth: 30
        );
        Console.WriteLine();
        DrawCard("Jan", "Dev", '?', minWidth: 40);

        // 6.
        Console.WriteLine("\nZadanie 6");
        var result = CountMyTypes(2, 3.5, "Hello", "abc", 10.2, 4, true, 7.0, "abcdef", -3);
        Console.WriteLine($"Parzyste int: {result.Evens}");
        Console.WriteLine($"Dodatnie double: {result.PositiveDoubles}");
        Console.WriteLine($"Min. 5 znaków: {result.LongStrings}");
        Console.WriteLine($"Inne typy: {result.Other}");



    }

    static void ShowPersonInfo((string Imie, string Nazwisko, int Wiek, double Placa) person)
    {
        Console.WriteLine("=== Sposób 1: Item1, Item2 itd. ===");
        Console.WriteLine($"Imię: {person.Item1}, Nazwisko: {person.Item2}, Wiek: {person.Item3}, Płaca: {person.Item4}");

        Console.WriteLine("\n=== Sposób 2: Dostęp po nazwach pól ===");
        Console.WriteLine($"Imię: {person.Imie}, Nazwisko: {person.Nazwisko}, Wiek: {person.Wiek}, Płaca: {person.Placa}");

        Console.WriteLine("\n=== Sposób 3: Dekonstrukcja krotki ===");
        var (imie, nazwisko, wiek, placa) = person;
        Console.WriteLine($"Imię: {imie}, Nazwisko: {nazwisko}, Wiek: {wiek}, Płaca: {placa}");
    }
    static void UseAnonymous(dynamic anon)
    {
        Console.WriteLine($"imie: {anon.Imie}, wiek: {anon.Wiek}, miasto: {anon.Miasto}");
    }

    static void DrawCard(
        string line1,
        string line2,
        char borderChar = '*',
        int borderWidth = 1,
        int minWidth = 20
    )
    {
        // Najdłuższy napis
        int textWidth = Math.Max(line1.Length, line2.Length);

        // Całkowita szerokość wnętrza (bez obramowania)
        int innerWidth = Math.Max(minWidth - 2 * borderWidth, textWidth);

        // Całkowita szerokość wizytówki
        int totalWidth = innerWidth + 2 * borderWidth;

        // Rysowanie górnej ramki
        for (int i = 0; i < borderWidth; i++)
            Console.WriteLine(new string(borderChar, totalWidth));

        // Linia 1 – wycentrowana
        Console.WriteLine(
            new string(borderChar, borderWidth)
            + CenterText(line1, innerWidth)
            + new string(borderChar, borderWidth)
        );

        // Linia 2 – wycentrowana
        Console.WriteLine(
            new string(borderChar, borderWidth)
            + CenterText(line2, innerWidth)
            + new string(borderChar, borderWidth)
        );

        // Rysowanie dolnej ramki
        for (int i = 0; i < borderWidth; i++)
            Console.WriteLine(new string(borderChar, totalWidth));
    }

    // CenterText – centrowanie napisu
    static string CenterText(string text, int width)
    {
        int spaces = width - text.Length;

        int left = spaces / 2;
        int right = spaces - left;

        return new string(' ', left) + text + new string(' ', right);
    }


    static (int Evens, int PositiveDoubles, int LongStrings, int Other)
        CountMyTypes(params object[] elements)
    {
        int evenInts = 0;
        int positiveDoubles = 0;
        int longStrings = 0;
        int other = 0;

        foreach (var el in elements)
        {
            switch (el)
            {
                case int i when i % 2 == 0:
                    evenInts++;
                    break;

                case int:
                    // int nieparzysty – nie liczymy, ale też nie jest „inne”
                    break;

                case double d when d > 0:
                    positiveDoubles++;
                    break;

                case double:
                    // double ujemne/zero – ignorujemy
                    break;

                case string s when s.Length >= 5:
                    longStrings++;
                    break;

                case string:
                    // krótkie stringi – ignorujemy
                    break;

                default:
                    other++;
                    break;
            }
        }

        return (evenInts, positiveDoubles, longStrings, other);
    }
}

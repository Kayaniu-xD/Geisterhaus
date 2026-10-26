using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Random zufall = new Random();
        const int groesse = 3;

        // Merkt sich, welche Räume schon durchsucht wurden (Bonus 3)
        bool[,] besucht = new bool[groesse, groesse];

        // Bonus 1: Geist bei jedem Programmstart zufällig platzieren
        int geistZeile = zufall.Next(0, groesse);
        int geistSpalte = zufall.Next(0, groesse);

        // Bonus 2: Der Spieler hat so viele Versuche, wie das Haus Zeilen hat
        int versucheUebrig = groesse;
        bool geistGefunden = false;

        Console.WriteLine("=== Willkommen im Geisterhaus! ===");
        Console.WriteLine($"Irgendwo in diesem {groesse}x{groesse}-Haus versteckt sich ein Geist.");
        Console.WriteLine($"Du hast {versucheUebrig} Versuche, ihn zu finden. Zeile und Spalte gehen von 1 bis {groesse}.\n");

        while (versucheUebrig > 0 && !geistGefunden)
        {
            Console.WriteLine($"--- Noch {versucheUebrig} Versuch(e) übrig ---");

            int zeileIndex = LiesZahlEin($"Wähle eine Zeile (1-{groesse}): ", groesse) - 1;
            int spalteIndex = LiesZahlEin($"Wähle eine Spalte (1-{groesse}): ", groesse) - 1;

            // Bonus 3: denselben Raum kein zweites Mal erlauben
            if (besucht[zeileIndex, spalteIndex])
            {
                Console.WriteLine("Diesen Raum hast du schon durchsucht. Wähle einen anderen Raum!\n");
                continue; // kein Versuch wird verbraucht
            }

            besucht[zeileIndex, spalteIndex] = true;

            if (zeileIndex == geistZeile && spalteIndex == geistSpalte)
            {
                Console.WriteLine("\nDu hast den Geist gefunden! Du gewinnst!");
                geistGefunden = true;
            }
            else
            {
                Console.WriteLine("Dieser Raum ist leer. Kein Geist hier.\n");
                versucheUebrig--;
            }
        }

        if (!geistGefunden)
        {
            Console.WriteLine("\nDu hast alle Versuche aufgebraucht. Der Geist entkommt!");
            Console.WriteLine($"Er versteckte sich in Zeile {geistZeile + 1}, Spalte {geistSpalte + 1}.");
        }

        Console.WriteLine("\nDrücke eine beliebige Taste zum Beenden...");
        Console.ReadKey();
    }

    /// Liest vom Spieler eine ganze Zahl zwischen 1 und max ein
    /// und wiederholt die Abfrage, bis eine gültige Eingabe gemacht wurde.
    private static int LiesZahlEin(string hinweistext, int max)
    {
        while (true)
        {
            Console.Write(hinweistext);

            if (int.TryParse(Console.ReadLine(), out int zahl) && zahl >= 1 && zahl <= max)
            {
                return zahl;
            }

            Console.WriteLine($"Ungültige Eingabe! Bitte eine Zahl zwischen 1 und {max} eingeben.");
        }
    }
}
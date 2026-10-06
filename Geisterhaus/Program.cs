using System;

namespace Geisterhaus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // für korrekte Darstellung von Umlauten

            const int groesse = 3;

            // 3x3-Array für das Geisterhaus (true = hier versteckt sich der Geist)
            bool[,] geisterhaus = new bool[groesse, groesse];

            // Merkt sich, welche Räume schon durchsucht wurden (Bonus 3)
            bool[,] besucht = new bool[groesse, groesse];

            // Bonus 1: Geist bei jedem Programmstart zufällig platzieren
            Random zufall = new Random();
            int geistZeile = zufall.Next(0, groesse);
            int geistSpalte = zufall.Next(0, groesse);
            geisterhaus[geistZeile, geistSpalte] = true;

            // Bonus 2: Der Spieler hat 3 Versuche
            int versucheUebrig = 3;
            bool geistGefunden = false;

            Console.WriteLine("=== Willkommen im Geisterhaus! ===");
            Console.WriteLine($"Irgendwo in diesem {groesse}x{groesse}-Haus versteckt sich ein Geist.");
            Console.WriteLine($"Du hast {versucheUebrig} Versuche, ihn zu finden. Zeile und Spalte gehen von 1 bis {groesse}.\n");

            while (versucheUebrig > 0 && !geistGefunden)
            {
                Console.WriteLine($"--- Noch {versucheUebrig} Versuch(e) übrig ---");

                int zeile = LiesZahlEin($"Wähle eine Zeile (1-{groesse}): ", groesse);
                int spalte = LiesZahlEin($"Wähle eine Spalte (1-{groesse}): ", groesse);

                int zeileIndex = zeile - 1;
                int spalteIndex = spalte - 1;

                // Bonus 3: denselben Raum kein zweites Mal erlauben
                if (besucht[zeileIndex, spalteIndex])
                {
                    Console.WriteLine("Diesen Raum hast du schon durchsucht. Wähle einen anderen Raum!\n");
                    continue; // kein Versuch wird verbraucht
                }

                besucht[zeileIndex, spalteIndex] = true;

                if (geisterhaus[zeileIndex, spalteIndex])
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
        static int LiesZahlEin(string hinweistext, int max)
        {
            int zahl;
            while (true)
            {
                Console.Write(hinweistext);
                string eingabe = Console.ReadLine();

                if (int.TryParse(eingabe, out zahl) && zahl >= 1 && zahl <= max)
                {
                    return zahl;
                }

                Console.WriteLine($"Ungültige Eingabe! Bitte eine Zahl zwischen 1 und {max} eingeben.");
            }
        }
    }
}
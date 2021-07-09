using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelDemo
{
    class Program
    {
        static void Main()
        {
            //Demo();

            /*-byte array erstellen
             *-befüllen mit Random Eurojackpot Lottozahlen
             *-Array für statistik erstellen 8 plätze
             *- [anzahl 0 richtige][anzahl 1 richtiger][anzahl 2 richtige][anzahl 3 richtige] ...
             *-funktion erstellen welche die Benutzerzahlen bekommt, random zahlen zieht und vergleicht
             * Solange noch keine 5 sekunden vergangen sind, funktion starten
             * Ausgabe der Statistik und anzahl der Ziehungen
             */

            List<byte> DauerSchein = new() { 3, 6, 12, 18, 21, 34, 41 };// ist sortiert, binärsuche?!
            int[] statistik = new int[8];

            // [0] 354891
            // [1] 549816
            // [2]  64823
            // [3]  10023
            // [4]   4823
            // [5]    823
            // [6]     90
            // [7]      0
            DateTime startZeit = DateTime.Now;
            while ((DateTime.Now - startZeit).TotalSeconds < 5)
            {
                statistik[WinningNumbers(DauerSchein)]++;
            }

            int sum = 0;
            foreach (var item in statistik)
            {
                Console.WriteLine(item);
                sum += item;
            }
            Console.WriteLine(sum);

            Console.ReadLine();
        }

        public static int WinningNumbers(List<byte> DauerSchein)
        {
            List<byte> Ziehung = generateNumbers();
            int rightNumberCount = 0;
            // vergleichen

            // Ziehung   5 3 7 9 1 12 17
            // Schein    7 3 5 1 9 17 12
            for (int i = 0; i < Ziehung.Count; i++)
            {
                for (int x = 0; x < DauerSchein.Count; x++)
                {
                    if (Ziehung[i] == DauerSchein[x])
                    {
                        rightNumberCount++;
                    }
                }
            }
            return rightNumberCount;
        }

        public static List<byte> generateNumbers()
        {
            List<byte> Ziehung = new(7);
            Random rndGen = new();
            byte RandomNumber;
            while (Ziehung.Count < 7)
            {
                RandomNumber = (byte)rndGen.Next(1, 50);
                if (!Ziehung.Contains(RandomNumber))
                    Ziehung.Add(RandomNumber);
            }
            return Ziehung;
        }

        private static void Demo()
        {
            Console.WriteLine("Parallel Invoke");
            Parallel.Invoke(
               () => printInt(5),
               () => printInt(2),
               () => printInt(7),
               () => printInt(3));

            Console.WriteLine("\n##################################################");

            Console.WriteLine("\nSerielles For (das normale dingen)");
            for (int i = 0; i < 50; i++) printInt(i);

            Console.WriteLine("\nParallel For (wie das normale, nur auf mehreren kernen)");
            Parallel.For(0, 50, printInt);

            Console.WriteLine("\n##################################################");

            int[] integerArray = new int[50];
            for (int counter = 0; counter < integerArray.Length; counter++) integerArray[counter] = counter;

            Console.WriteLine("\n Normales foreach");
            foreach (var item in integerArray) printInt(item);

            Console.WriteLine("\n Parralel Foreach");
            Parallel.ForEach(integerArray, printInt);

            _ = Console.ReadLine();
        }

        static void printInt(int IntToPrint)
        {
            Console.Write($"{IntToPrint,3}");
        }
    }
}

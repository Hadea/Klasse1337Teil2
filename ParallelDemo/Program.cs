using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelDemo
{
    internal static class Program
    {
        private static List<byte> DauerSchein;

        private static void Main()
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

            DauerSchein = new() { 12, 18, 21, 34, 41, 3, 6 };// ist sortiert, binärsuche?!
            int[] statistik = new int[8];

            // [0] 354891
            // [1] 549816
            // [2]  64823
            // [3]  10023
            // [4]   4823
            // [5]    823
            // [6]     90
            // [7]      0

            byte threadcount = 16;
            List<int[]> statistikListe = new();
            for (int counter = 0; counter < threadcount; counter++)
            {
                statistikListe.Add(new int[8]);
            }

            _ = Parallel.ForEach(statistikListe, FuenfSekundenLotto);

            for (int count = 0; count < statistik.Length; count++)
            {
                for (int inner = 0; inner < statistikListe.Count; inner++)
                {
                    int[] statistikListItem = statistikListe[inner];
                    statistik[count] += statistikListItem[count];
                }
            }

            int sum = 0;
            foreach (var item in statistik)
            {
                Console.WriteLine(item);
                sum += item;
            }
            Console.WriteLine(sum.ToString("N0"));

            _ = Console.ReadLine();
        }

        private static void FuenfSekundenLotto(int[] statistik)
        {
            DateTime startZeit = DateTime.Now;
            while ((DateTime.Now - startZeit).TotalSeconds < 5)
            {
                statistik[WinningNumbers(DauerSchein)]++;
            }
        }

        public static int WinningNumbers(List<byte> DauerSchein)
        {
            List<byte> Ziehung = generateNumbers();
            int rightNumberCount = 0;
            // vergleichen

            // Ziehung   5 3 7 9 1 12 17
            // Schein    7 3 5 1 9 17 12
            for (int i = 0; i < 5; i++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (Ziehung[i] == DauerSchein[x])
                    {
                        rightNumberCount++;
                    }
                }
            }
            
            if (Ziehung[5] == DauerSchein[5] || Ziehung[5] == DauerSchein[6])// Ziehung 5 vergleichen mit Dauerschein 5 und 6
                        rightNumberCount++;
            if (Ziehung[6] == DauerSchein[5] || Ziehung[6] == DauerSchein[6])// Ziehung 6 vergleichen mit Dauerschein 5 und 6
                        rightNumberCount++;

            
            // Alternative mit schleifen
            /*for (int i = 5; i < 7; i++)
            {
                for (int x = 5; x < 7; x++)
                {
                    if (Ziehung[i] == DauerSchein[x])
                    {
                        rightNumberCount++;
                    }
                }
            }*/
            return rightNumberCount;
        }

        public static List<byte> generateNumbers()
        {
            List<byte> Ziehung = new(7);

            Random rndGen = new();
            byte RandomNumber;
            while (Ziehung.Count < 5)
            {
                RandomNumber = (byte)rndGen.Next(1, 51); //ab 1 inclusive, unterhalb 51
                if (!Ziehung.Contains(RandomNumber))
                    Ziehung.Add(RandomNumber);
            }


            Ziehung.Add((byte)rndGen.Next(1, 11));//ab 1 inclusive, unterhalb 11

            do
            {
                RandomNumber = (byte)rndGen.Next(1, 11);//ab 1 inclusive, unterhalb 11
            }
            while (RandomNumber == Ziehung[5]) ;

            Ziehung.Add(RandomNumber);
            /*
             *  Erster weg die Superzahlen zu ziehen
            List<byte> ZiehungSuperzahl = new(2);
            while (ZiehungSuperzahl.Count < 2)
            {
                RandomNumber = (byte)rndGen.Next(1, 11);//ab 1 inclusive, unterhalb 11
                if (!ZiehungSuperzahl.Contains(RandomNumber))
                    ZiehungSuperzahl.Add(RandomNumber);
            }

            Ziehung.AddRange(ZiehungSuperzahl);
            */
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

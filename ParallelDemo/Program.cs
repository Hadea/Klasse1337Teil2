using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelDemo
{
    internal static class Program
    {

        private static void Main()
        {
            //Demo();

            Lotto6Aus49 lotto6Aus49 = new();
            lotto6Aus49.Ticket = new() { 12, 18, 21, 34, 41, 3, 6 };

            LottoEurojackpot lottoEurojackpot = new();
            lottoEurojackpot.Ticket = new() { 12, 18, 21, 34, 41, 3, 6 };

            TimeTest(lottoEurojackpot);
            /*
            List<LottoWorkPackage> WorkPackageList = new();

            int desiredTries = 100_000_000;
            
            for (int counter = 0; counter < desiredTries / 10000; counter++)
            {
                LottoWorkPackage wp;
                wp.Compare = lotto6Aus49.Compare;
                wp.Prefill = lotto6Aus49.Prefill;
                wp.Ticket = lotto6Aus49.Ticket;
                wp.Statistik = new int[8];
                WorkPackageList.Add(wp);
            }

            for (int counter = 0; counter < desiredTries / 10000; counter++)
            {
                LottoWorkPackage wp;
                wp.Compare = lottoEurojackpot.Compare;
                wp.Prefill = lottoEurojackpot.Prefill;
                wp.Ticket = lottoEurojackpot.Ticket;
                wp.Statistik = new int[8];
                WorkPackageList.Add(wp);
            }
            */
            //LottoWorkPackage wp = new() { Ticket = lottoEurojackpot.Ticket, Compare = lottoEurojackpot.Compare, Prefill = lottoEurojackpot.Prefill, Statistik = new int[8] };
            //Lotto10kSpielen(wp);
            /*
             _ = Parallel.ForEach(WorkPackageList, LottoWorkPackage.Lotto10kSpielen);

            for (int counter = 0; counter < desiredTries / 10000; counter++)
            {
                for (int inner = 0; inner < lotto6Aus49.Statistik.Length; inner++)
                {
                    lotto6Aus49.Statistik[inner] += WorkPackageList[counter].Statistik[inner];
                }
            }
            
            for (int counter = desiredTries / 10000; counter < desiredTries / 10000 * 2; counter++)
            {
                for (int inner = 0; inner < lottoEurojackpot.Statistik.Length; inner++)
                {
                    lottoEurojackpot.Statistik[inner] += WorkPackageList[counter].Statistik[inner];
                }
            }

            int sum = 0;

            Console.WriteLine("Statistik für Lotto 6 Aus 49");
            for (int counter = 0; counter < lotto6Aus49.Statistik.Length; counter++)
            {
                Console.WriteLine($" {counter} Richtige: {lotto6Aus49.Statistik[counter]:N0}");
                sum += lotto6Aus49.Statistik[counter];
            }
            Console.WriteLine("Anzahl der Ziehungen: " + sum.ToString("N0"));

            sum = 0;
            Console.WriteLine("\nStatistik für Eurojackpot");
            for (int counter = 0; counter < lottoEurojackpot.Statistik.Length; counter++)
            {
                Console.WriteLine($" {counter} Richtige: {lottoEurojackpot.Statistik[counter]:N0}");
                sum += lottoEurojackpot.Statistik[counter];
            }
            Console.WriteLine("Anzahl der Ziehungen: " + sum.ToString("N0"));*/
            _ = Console.ReadLine();
        }

        private static void TimeTest(LottoEurojackpot lottoEurojackpot)
        {
            int[] Statistik = new int[8];
            List<byte> Ziehung = new();

            DateTime startZeit = DateTime.Now;
            for (int counter = 0; counter < 1_000_000; counter++)
            {
                lottoEurojackpot.Prefill(Ziehung);
                Statistik[lottoEurojackpot.Compare(lottoEurojackpot.Ticket, Ziehung)]++;
            }
            DateTime endZeit = DateTime.Now;
            Console.WriteLine((endZeit - startZeit).TotalSeconds);
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

using System;
using System.Threading.Tasks;

namespace MultithreadCounter
{
    internal static class Program
    {
        private static void Main()
        {
            // array erstellen. Der Array Operator arbeitet mit int, demnach sind nur 2,14 milliarden stellen erlaubt
            byte[] arrayToSum = new byte[1_000_000_000];

            // zufallsgenerator erstellen
            Random rndGen = new();
            // der zufallsgenerator kann byte-arrays direkt befüllen. währe es ein anderer Datentyp wird eine schleife benötigt
            rndGen.NextBytes(arrayToSum);

            #region SingleThread

            // summe für den einzelnen thread. da die gesamtsumme über 2,14 milliarden sein kann wird long verwendet
            long sumSingleThread = 0;
            foreach (byte item in arrayToSum) sumSingleThread += item;

            #endregion

            // Erstellen eines Array welches die Threads steuert
            Task[] tasks = new Task[100];

            #region Multithreading Version 1

            ThreadData[] dataArray = new ThreadData[tasks.Length];

            for (int taskID = 0; taskID < tasks.Length; taskID++)
            {
                ThreadData currentData = new()
                {
                    Anzahl = arrayToSum.Length / tasks.Length,
                    Beginn = arrayToSum.Length / tasks.Length * taskID,
                    ArrayToSum = arrayToSum
                };
                dataArray[taskID] = currentData;

                tasks[taskID] = new Task(() => sumOfArrayPart(currentData));
            }

            foreach (Task item in tasks) item.Start();

            Task.WaitAll(tasks);


            // vorbereiten der gesamtsumme
            long sumMultiThread = 0;
            // ziehen der summe über die Teilaufgaben
            foreach (ThreadData item in dataArray) sumMultiThread += item.Summe;
            #endregion

            #region Multithreading Version 2 (mit lock)
            // in dieser version addieren die threads ihre Teilsumme zur gesamtsumme selbst
            // es besteht die gefahr das sie das gleichzeitig machen und es dadurch zu datenverlust kommt
            // das lock verhindert dies indem für die addition das multithreading verboten wird

            long sumMultiThreadv2 = 0;

            // das lock kann jeder referenz-typ sein. damit nicht ausversehen ein objekt für mehr als ein
            // lock benutzt wird sollte jeder anwendungsfall ein eigenes bekommen
            object sumMultiThreadv2Lock = new();

            for (int taskID = 0; taskID < tasks.Length; taskID++)
            {
                // da das Lambda erst nach der For-Schleife ausgeführt wird liesst sie auch erst dann
                // die Zählvariable ein. durch das erstellen einer kopie für jeden einzelnen task bekommt
                // jedes lambda auch einen eigenen wert
                int taskIDcopy = taskID;

                tasks[taskID] = new Task(() =>
                {
                    int beginn = arrayToSum.Length / tasks.Length * taskIDcopy;
                    long threadSum = 0;
                    for (int counter = beginn; counter < beginn + (arrayToSum.Length / tasks.Length); counter++)
                    {
                        threadSum += arrayToSum[counter];
                    }
                    lock (sumMultiThreadv2Lock) // hier wird für alle threads welche das gleiche lock benutzen in den seriellen modus geschaltet
                    {
                        sumMultiThreadv2 += threadSum;
                    }
                });
            }
            foreach (Task item in tasks) item.Start();
            Task.WaitAll(tasks);
            #endregion

            Console.WriteLine($"Summe Singlethread: {sumSingleThread}");
            Console.WriteLine($"Summe Multithread : {sumMultiThread}");
            Console.WriteLine($"Summe Multithread : {sumMultiThreadv2}");
            _ = Console.ReadLine();
        }

        private static void sumOfArrayPart(ThreadData data)
        {
            long sum = 0;
            for (int counter = data.Beginn; counter < data.Beginn + data.Anzahl; counter++)
            {
                sum += data.ArrayToSum[counter];
            }
            data.Summe = sum;
        }
    }

    internal class ThreadData
    {
        // alles was der Thread braucht um eigenständig zu arbeiten
        // wenn das original-array nur gelesen wird geht eine referenz, andernfalls sollte der
        // thread auf einer Kopie arbeiten
        public byte[] ArrayToSum;
        public int Beginn;
        public int Anzahl;
        public long Summe;
    }
}

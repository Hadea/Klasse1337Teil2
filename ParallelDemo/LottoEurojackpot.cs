using System;
using System.Collections.Generic;

namespace ParallelDemo
{
    internal class LottoEurojackpot : Lotto
    {

        public LottoEurojackpot()
        {
            Statistik = new int[8];
        }
        public override int Compare(List<byte> TicketA, List<byte> TicketB)
        {
            if (TicketA is null) throw new ArgumentNullException(nameof(TicketB));
            if (TicketA.Count != 7) throw new ArgumentException("Ungültige anzahl an Elementen", nameof(TicketA));
            if (TicketB is null) throw new ArgumentNullException(nameof(TicketB));
            if (TicketB.Count != 7) throw new ArgumentException("Ungültige anzahl an Elementen", nameof(TicketB));

            int rightNumberCount = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (TicketA[i] == TicketB[x])
                    {
                        rightNumberCount++;
                    }
                }
            }

            if (TicketA[5] == TicketB[5] || TicketA[5] == TicketB[6])// Ziehung 5 vergleichen mit Dauerschein 5 und 6
                rightNumberCount++;
            if (TicketA[6] == TicketB[5] || TicketA[6] == TicketB[6])// Ziehung 6 vergleichen mit Dauerschein 5 und 6
                rightNumberCount++;

            return rightNumberCount;
        }

        public override void Prefill(List<byte> TicketToFill)
        {
            if (TicketToFill is null)
                TicketToFill = new(7);
            else
                TicketToFill.Clear();
            Random rndGen = new();
            byte RandomNumber;
            while (TicketToFill.Count < 5)
            {
                RandomNumber = (byte)rndGen.Next(1, 51); //ab 1 inclusive, unterhalb 51
                if (!TicketToFill.Contains(RandomNumber))
                    TicketToFill.Add(RandomNumber);
            }

            TicketToFill.Add((byte)rndGen.Next(1, 11));//ab 1 inclusive, unterhalb 11

            do
            {
                RandomNumber = (byte)rndGen.Next(1, 11);//ab 1 inclusive, unterhalb 11
            }
            while (RandomNumber == TicketToFill[5]);

            TicketToFill.Add(RandomNumber);
        }
    }
}

using System;
using System.Collections.Generic;

namespace ParallelDemo
{
    class Lotto6Aus49 : Lotto
    {
        public Lotto6Aus49()
        {
            Statistik = new int[8];
        }
        public override int Compare(List<byte> TicketA, List<byte> TicketB)
        {
            int rightNumberCount = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int x = 0; x < 6; x++)
                {
                    if (TicketA[i] == TicketB[x])
                    {
                        rightNumberCount++;
                    }
                }
            }

            if (TicketA[6] == TicketB[6])
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
            while (TicketToFill.Count < 6)
            {
                RandomNumber = (byte)rndGen.Next(1, 50); //ab 1 inclusive, unterhalb 50
                if (!TicketToFill.Contains(RandomNumber))
                    TicketToFill.Add(RandomNumber);
            }

            TicketToFill.Add((byte)rndGen.Next(10));//ab 0 inclusive, unterhalb 10
        }
    }
}

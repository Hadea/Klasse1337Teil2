using System;
using System.Collections.Generic;

namespace ParallelDemo
{
    struct LottoWorkPackage
    {
        public List<byte> Ticket;
        public int[] Statistik;
        public Action<List<byte>> Prefill;
        public Func<List<byte>, List<byte>, int> Compare;

        public static void Lotto10kSpielen(LottoWorkPackage wp)
        {
            List<byte> DrawnTicket = new(7);

            for (int counter = 0; counter < 10_000; counter++)
            {
                wp.Prefill(DrawnTicket);
                int result = wp.Compare(wp.Ticket, DrawnTicket);
                //Console.Write(result);
                wp.Statistik[result]++;
            }
        }
    }
}

using System.Collections.Generic;

namespace ParallelDemo
{
    internal abstract class Lotto
    {
        public List<byte> Ticket;
        public int[] Statistik;

        public abstract void Prefill(List<byte> TicketToFill);
        public abstract int Compare(List<byte> TicketA, List<byte> TicketB);
    }
}

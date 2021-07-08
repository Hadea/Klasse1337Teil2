using System;
using System.ComponentModel;

namespace BenchmarkViewModel
{
    public class AlgorithmViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public int Threads { get; set; }
        public bool Recursion { get; set; }
        public string Description { get; set; }
        public Action<byte[]> Method { get; set; }
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BenchmarkViewModel
{
    public class BenchmarkViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<AlgorithmViewModel> AlgorithmList { get; init; }
        public ObservableCollection<TestResultViewModel> ResultList { get; init; }

        public AlgorithmViewModel SelectedAlgorithm
        {
            get => selectedAlgorithm;
            set
            {
                if (selectedAlgorithm == value) return;
                selectedAlgorithm = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedAlgorithm)));
            }
        }
        private AlgorithmViewModel selectedAlgorithm;

        public ICommand StartSelectedAlgorithm { get; init; }

        public BenchmarkViewModel()
        {
            // load all algorithms into list
            AlgorithmList = new();
            //Hack: placeholder
            AlgorithmList.Add(new() { Name = "Placeholder", Description = "No Description", Threads = 1, Method = (_) => Thread.Sleep(5000) });
            AlgorithmList.Add(new() { Name = "Placeholder", Description = "No Description", Threads = 2, Method = (_) => Thread.Sleep(5000) });
            AlgorithmList.Add(new() { Name = "Placeholder", Description = "No Description", Threads = 8, Method = (_) => Thread.Sleep(5000) });

            ResultList = new();
            ResultList.Add(new() { Description = "exampleresult", ElapsedTime = TimeSpan.FromMilliseconds(2500) });
            ResultList.Add(new() { Description = "exampleresult", ElapsedTime = TimeSpan.FromMilliseconds(2000) });
            ResultList.Add(new() { Description = "exampleresult", ElapsedTime = TimeSpan.FromMilliseconds(5000) });
            ResultList.Add(new() { Description = "exampleresult", ElapsedTime = TimeSpan.FromMilliseconds(2500) });

            StartSelectedAlgorithm = new DelegateCommand(startAlgorithm);
        }

        private void startAlgorithm(object Algorithm)
        {
            if (Algorithm is not AlgorithmViewModel) return;
            Task.Run(() =>(Algorithm as AlgorithmViewModel).Method);

        }

    }
}

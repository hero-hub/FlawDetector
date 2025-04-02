using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace FlawDetector
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<string> _dataFiles = new List<string>();
        private LineGraph _lineGraph;
        private Dictionary<int, int> Flaws = new Dictionary<int, int>();
        public MainViewModel(LineGraph lineGraph)
        {
            _lineGraph = lineGraph;
            Visualization();
            SearchFlaw();
        }
        public (double[] x, double[] y) TxtToArrays(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            double[] y = lines.Select(Convert.ToDouble).ToArray();
            double[] x = Enumerable.Range(0, y.Length).Select(i => i / 100.0).ToArray();

            return (x, y);
        }
        private async Task Visualization()
        {
            for (int i = 0; i < 1057; i++)
            {
                string filePath = Path.Combine(@"D:\WORK\FlawDetector\signals\", $"{i}_CH-1_OnWr-2.txt");
                var (x, y) = TxtToArrays(filePath);

                if (x.Length > 0 && y.Length > 0)
                {
                    _lineGraph.Plot(x, y);
                }

                await Task.Delay(5);
            }
        }

        private void SearchFlaw()
        {
            int flawStart = 0;
            int flawEnd = 0;
            for (int i = 0; i < 1057; i++)
            {
                string filePath = Path.Combine(@"D:\WORK\FlawDetector\signals\", $"{i}_CH-1_OnWr-2.txt");
                var (x, y) = TxtToArrays(filePath);

                for (int j = 630; j < 831; j++)
                {
                    if (y[j] > 7)
                    {   
                        if (flawStart == 0) flawStart = i;
                        flawEnd = i;
                        break;
                    }
                }
                
                Flaws.Add(flawStart, flawEnd);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
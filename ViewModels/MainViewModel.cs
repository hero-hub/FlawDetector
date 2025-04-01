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
        
        public MainViewModel(LineGraph lineGraph)
        {
            _lineGraph = lineGraph;
            StartVisualization();
        }

        private async Task StartVisualization()
        {
            for (int i = 0; i < 1057; i++)
            {
                string filePath = Path.Combine(@"D:\WORK\FlawDetector\signals\", $"{i}_CH-1_OnWr-2.txt");
                string[] lines = File.ReadAllLines(filePath);

                double[] y = lines.Select(Convert.ToDouble).ToArray();
                double[] x = Enumerable.Range(0, y.Length)
                                      .Select(j => j / 100.0)
                                      .ToArray();

                _lineGraph.Plot(x, y);
                await Task.Delay(5);

                //for (int j = 630; j < 831; j++)
                //{
                    
                //}
            }
        }

        //private void SearchFlaw()
        //{
        //    for (int i = 630; i < 831; i++)
        //    {
        //        if (y[i] > 7)
        //        {
        //            while (y[i++] > 7) 
        //        }
        //    }
        //}
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
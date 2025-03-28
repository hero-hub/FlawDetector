using InteractiveDataDisplay.WPF;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace FlawDetector
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<string> _dataFiles = new List<string>();

        public MainViewModel()
        {
            StartVisualization();
        }

        private void StartVisualization()
        {
            string directoryPath = @"D:\WORK\FlawDetector\signals";
            _dataFiles = Directory.GetFiles(directoryPath, "*.txt").ToList();

            List<double> x = new List<double>(); //Array.Empty<double>();
            List<double> y = new List<double>(); //Array.Empty<double>();

            // create the lines and describe their styling
            var graf = new InteractiveDataDisplay.WPF.LineGraph
            {
                Stroke = new SolidColorBrush(Colors.Blue),
                Description = "Line A",
                StrokeThickness = 2
            };

            foreach( var line in _dataFiles)
            {
                string[] values = File.ReadAllLines(line);
                //Array.Clear(x);
                //Array.Clear(y);

                for (int i = 0; i < values.Length; i++)
                {
                    y.Add(Convert.ToDouble(values[i]));//ошибка выход за пределы массива
                    x.Add(Convert.ToDouble(i) / 100.0);
                }

                // load data into the lines
                graf.Plot(x, y);

                // add lines into the grid
                myGrid.Children.Clear();
                myGrid.Children.Add(graf);

                //// customize styling
                myChart.Title = $"Line Plot ({x:n0} points each)";
                myChart.BottomTitle = $"Horizontal Axis Label";
                myChart.LeftTitle = $"Vertical Axis Label";
                myChart.IsAutoFitEnabled = true;
                myChart.LegendVisibility = Visibility.Visible;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
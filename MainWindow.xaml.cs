using InteractiveDataDisplay.WPF;
using System.Windows;

namespace FlawDetector
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(MainGraph);
            //Loaded += MainWindow_Loaded;
        }

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DataContext = new MainViewModel(MainGraph);
        //}
    }
}
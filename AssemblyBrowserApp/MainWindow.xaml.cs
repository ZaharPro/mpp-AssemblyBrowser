using AssemblyBrowserLib;
using System.Windows;

namespace AssemblyBrowserApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowViewModel = new();
            DataContext = mainWindowViewModel;
            mainWindowViewModel.AssemblyInfo = typeof(MainWindow).Assembly.GetAssemblyInfo();
        }
    }
}

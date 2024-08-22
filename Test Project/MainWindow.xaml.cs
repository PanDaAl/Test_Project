using System.Windows;

namespace Test_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new DatabasePage();
        }
    }
}
using BreweryWPF.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BreweryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCreateBeerWindowButton_Click(object sender, RoutedEventArgs e)
        {
            CreateBeerWindow createBeerWindow = new CreateBeerWindow();
            if (createBeerWindow.ShowDialog() == true)
            {
                // Update your UI with the new beer
            }
        }
    }

}
using BreweryWPF.Services;
using BreweryWPF.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Xunit.Abstractions;

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

        private void OpenCreateBreweryButton_Click(object sender, RoutedEventArgs e)
        {
            CreateBreweryWindow createBreweryWindow = new CreateBreweryWindow();
            if (createBreweryWindow.ShowDialog() == true)
            {
                // Update your UI with the new beer
            }
        }

        private void OpenCreateWholesalerButton_Click(object sender, RoutedEventArgs e)
        {
            CreateWholesalerWindow createWholesalerWindow = new CreateWholesalerWindow();
            if (createWholesalerWindow.ShowDialog() == true)
            {
                // Update your UI with the new beer
            }
        }
    }

}
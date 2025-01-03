using BreweryAPI.DTO;
using BreweryWPF.Services;
using System.Windows;


namespace BreweryWPF
{
    /// <summary>
    /// Interaction logic for CreateBeer.xaml
    /// </summary>
    public partial class CreateWholesalerWindow : Window
    {
        private WholesalerService _wholesalerService;


        public CreateWholesalerWindow()
        {
            InitializeComponent();
            _wholesalerService = new("https://localhost:5000/");
        }

        private async void CreateWholesalerButton_Click(object sender, RoutedEventArgs e)
        {
            string wholesalerName = WholesalerTextBox.Text;

            if (string.IsNullOrEmpty(wholesalerName))
            {
                MessageBox.Show("Please provide brewery name.");
                return;
            }

            bool result = await _wholesalerService.CreateWholesalerAsync(wholesalerName);

            if (result)
            {
                MessageBox.Show("Wholesaler added successfully!");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Failed to add wholesaler.");
            }
        }

    }
}

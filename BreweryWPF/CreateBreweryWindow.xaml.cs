using BreweryAPI.DTO;
using BreweryWPF.Services;
using System.Windows;


namespace BreweryWPF
{
    /// <summary>
    /// Interaction logic for CreateBeer.xaml
    /// </summary>
    public partial class CreateBreweryWindow : Window
    {
        private BreweryService _breweryService;
        private List<BreweryDTO> breweries;

        public CreateBreweryWindow()
        {
            InitializeComponent();
            _breweryService = new("https://localhost:5000/");
        }

        private async void CreateBreweryButton_Click(object sender, RoutedEventArgs e)
        {
            string breweryName = BreweryTextBox.Text;

            if (string.IsNullOrEmpty(breweryName))
            {
                MessageBox.Show("Please provide brewery name.");
                return;
            }

            bool result = await _breweryService.CreateBreweryAsync(breweryName);

            if (result)
            {
                MessageBox.Show("Brewery added successfully!");
                this.DialogResult = true; // Close window with success signal
            }
            else
            {
                MessageBox.Show("Failed to add brewery.");
            }
        }

    }
}

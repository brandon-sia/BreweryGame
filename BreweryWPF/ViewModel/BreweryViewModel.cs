using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BreweryAPI.DTO;
using BreweryAPI.Models;
using BreweryWPF.Services;

namespace BreweryWPF.ViewModel
{
    public class BreweryViewModel : INotifyPropertyChanged
    {
        private readonly BreweryService _breweryService;
        public ICommand RefreshCommand { get; }

        public ObservableCollection<BreweryDTO> Breweries { get; set; } = new ObservableCollection<BreweryDTO>();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public BreweryViewModel()
        {
            // Set the API base address
            _breweryService = new BreweryService("https://localhost:5000/");
            LoadBreweries();
            RefreshCommand = new RelayCommand(LoadBreweries);
        }

        public async void LoadBreweries()
        {
            IsLoading = true;
            try
            {
                var breweries = await _breweryService.GetBreweriesAsync();
                Breweries.Clear();
                foreach (var brewery in breweries)
                {
                    Breweries.Add(brewery);
                }
            }
            catch { }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

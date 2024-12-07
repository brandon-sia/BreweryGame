using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BreweryAPI.Models;
using BreweryWPF.Services;

namespace BreweryWPF.ViewModel
{
    public class BeerViewModel : INotifyPropertyChanged
    {
        private readonly BeerService _beerService;

        public ObservableCollection<BeerDTO> Beers { get; set; } = new ObservableCollection<BeerDTO>();

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

        public BeerViewModel()
        {
            // Set the API base address
            _beerService = new BeerService("https://localhost:5000/");
            LoadBeers();
        }

        public async void LoadBeers()
        {
            IsLoading = true;
            try
            {
                var beers = await _beerService.GetBeersAsync();
                Beers.Clear();
                foreach (var beer in beers)
                {
                    Beers.Add(beer);
                }
            }
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

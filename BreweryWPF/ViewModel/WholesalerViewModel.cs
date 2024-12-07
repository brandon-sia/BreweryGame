using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BreweryAPI.DTO;
using BreweryAPI.Models;
using BreweryWPF.Services;

namespace BreweryWPF.ViewModel
{
    public class WholesalerViewModel : INotifyPropertyChanged
    {
        private readonly WholesalerService _wholesalerService;

        public ObservableCollection<WholesalerDTO> Wholesalers { get; set; } = new ObservableCollection<WholesalerDTO>();

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

        public WholesalerViewModel()
        {
            // Set the API base address
            _wholesalerService = new WholesalerService("https://localhost:5000/");
            LoadWholesalers();
        }

        public async void LoadWholesalers()
        {
            IsLoading = true;
            try
            {
                var wholesalers = await _wholesalerService.GetWholesalerAsync();
                Wholesalers.Clear();
                foreach (var wholesaler in wholesalers)
                {
                    Wholesalers.Add(wholesaler);
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

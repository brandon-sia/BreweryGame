using AutoMapper;
using BreweryAPI.Controllers;
using BreweryAPI.DTO;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using BreweryWPF.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BreweryWPF
{
    /// <summary>
    /// Interaction logic for CreateBeer.xaml
    /// </summary>
    public partial class CreateBeerWindow : Window
    {
        private BeerService _beerService;
        private BreweryService _breweryService;
        private List<BreweryDTO> breweries;

        public CreateBeerWindow()
        {
            InitializeComponent();
            _beerService = new("https://localhost:5000/");
            _breweryService = new("https://localhost:5000/");
            LoadBreweries();
        }

        // Load breweries into the ComboBox
        private async void LoadBreweries()
        {
            breweries = await _breweryService.GetBreweriesAsync();
            var breweryNames = breweries.Select(b => b.Name).ToList();
            BreweryComboBox.ItemsSource = breweryNames;
        }

        private async void CreateBeerButton_Click(object sender, RoutedEventArgs e)
        {
            string beerName = BeerTextBox.Text;
            string selectedBrewery = BreweryComboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(beerName) || string.IsNullOrEmpty(selectedBrewery))
            {
                MessageBox.Show("Please provide both beer name and brewery.");
                return;
            }

            int breweryId = breweries.Where(b=>b.Name == selectedBrewery).FirstOrDefault().Id;


            if (breweryId == null)
            {
                MessageBox.Show("Brewery not found.");
                return;
            }



            bool result = await _beerService.CreateBeerAsync(beerName, breweryId);

            if (result)
            {
                MessageBox.Show("Beer added successfully!");
                this.DialogResult = true; // Close window with success signal
            }
            else
            {
                MessageBox.Show("Failed to add beer.");
            }
        }
    }
}

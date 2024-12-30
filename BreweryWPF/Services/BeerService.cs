using BreweryAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;


namespace BreweryWPF.Services
{
    class BeerService
    {
        private readonly HttpClient _httpClient;

        public BeerService(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri(baseAddress) };

        }

        public async Task<List<BeerDTO>> GetBeersAsync()
        {
                var response = await _httpClient.GetAsync("api/beer");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<List<BeerDTO>>(jsonResponse, options);
            }

        public async Task<bool> CreateBeerAsync(string name, int breweryId)
        {

            Beer beer = new Beer
            {
                Name = name,
                BrewerId = breweryId,
            };

            var response = await _httpClient.PostAsJsonAsync($"api/beer?brewerId={breweryId}", beer);

            return response.IsSuccessStatusCode;
        }

    }
}

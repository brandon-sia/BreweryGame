using BreweryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            return JsonSerializer.Deserialize<List<BeerDTO>>(jsonResponse, options);
        }

    }
}

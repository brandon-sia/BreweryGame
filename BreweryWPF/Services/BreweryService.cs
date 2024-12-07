using BreweryAPI.DTO;
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
    class BreweryService
    {
        private readonly HttpClient _httpClient;

        public BreweryService(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri(baseAddress) };

        }

        public async Task<List<BreweryDTO>> GetBreweriesAsync()
        {
            var response = await _httpClient.GetAsync("api/brewery");
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BreweryDTO>>(jsonResponse, options);
        }

    }
}

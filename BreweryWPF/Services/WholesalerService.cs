using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using BreweryAPI.DTO;
using BreweryAPI.Models;


namespace BreweryWPF.Services
{
    class WholesalerService
    {
        private readonly HttpClient _httpClient;

        public WholesalerService(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri(baseAddress) };

        }

        public async Task<List<WholesalerDTO>> GetWholesalerAsync()
        {
                var response = await _httpClient.GetAsync("api/wholesaler");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<WholesalerDTO>>(jsonResponse, options);
        }

        public async Task<bool> CreateWholesalerAsync(string name)
        {

            Wholesaler wholesaler = new Wholesaler
            {
                Name = name
            };

            var response = await _httpClient.PostAsJsonAsync($"api/wholesaler", wholesaler);

            return response.IsSuccessStatusCode;
        }


    }
}

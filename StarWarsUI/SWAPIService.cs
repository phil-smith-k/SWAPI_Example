using StarWarsUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsUI
{
    public class SWAPIService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<Person> GetPersonAsync(string url)
        {
            var personResponse = await _httpClient.GetAsync(url);
            if (personResponse.IsSuccessStatusCode)
            {
                Person person = await personResponse.Content.ReadAsAsync<Person>();
                return person;
            }
            return null;
        }
        public async Task<Vehicle> GetVehicleAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Vehicle>();
            }
            return null;
        }
        // Generic Method for GetAsync 
        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default;
        }
        // Generic Search Result Method
        public async Task<SearchResult<T>> GetSearchAsync<T>(string category, string query)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://swapi.co/api/{category}/?search={query}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<T>>();
            }
            return default;
        }

        public async Task<SearchResult<Person>> GetPersonSearchAsync(string query) => await GetSearchAsync<Person>("people", query);
    }
}

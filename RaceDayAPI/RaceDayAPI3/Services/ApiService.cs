using System.Text;
using System.Text.Json;

namespace RaceDay.MVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("RaceDayApi");
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode) return default;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch { return default; }
        }
        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(endpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> PostMultipartAsync(string endpoint, HttpContent multipartContent)
        {
            try
            {
                var response = await _client.PostAsync(endpoint, multipartContent);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}

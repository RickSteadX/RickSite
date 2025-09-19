using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PersonalWebsite.Client.Models;
using System.Text.Json;
using System.Text;

namespace PersonalWebsite.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private const string AuthUrl = "api/auth";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var loginRequest = new
            {
                Username = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync($"{AuthUrl}/login", loginRequest);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }

            throw new Exception("Login failed: " + await response.Content.ReadAsStringAsync());
        }

        public async Task LogoutAsync()
        {
            var response = await _httpClient.PostAsync($"{AuthUrl}/logout", null);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Logout failed: " + await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<User> GetCurrentUserAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{AuthUrl}/current");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }
                
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
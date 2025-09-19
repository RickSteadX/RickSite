using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PersonalWebsite.Client.Models;

namespace PersonalWebsite.Client.Services
{
    public class PageService
    {
        private readonly HttpClient _httpClient;
        private const string PagesUrl = "api/pages";

        public PageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Page>> GetPagesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Page>>(PagesUrl);
        }

        public async Task<List<Page>> GetAllPagesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Page>>($"{PagesUrl}/all");
        }

        public async Task<Page> GetPageAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Page>($"{PagesUrl}/{id}");
        }

        public async Task<Page> GetPageBySlugAsync(string slug)
        {
            return await _httpClient.GetFromJsonAsync<Page>($"{PagesUrl}/slug/{slug}");
        }

        public async Task<List<object>> GetNavigationAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<object>>($"{PagesUrl}/navigation");
        }

        public async Task<Page> CreatePageAsync(Page page)
        {
            var response = await _httpClient.PostAsJsonAsync(PagesUrl, page);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Page>();
        }

        public async Task UpdatePageAsync(int id, Page page)
        {
            var response = await _httpClient.PutAsJsonAsync($"{PagesUrl}/{id}", page);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePageAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{PagesUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
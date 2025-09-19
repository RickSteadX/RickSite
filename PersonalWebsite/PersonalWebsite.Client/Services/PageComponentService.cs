using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PersonalWebsite.Client.Models;

namespace PersonalWebsite.Client.Services
{
    public class PageComponentService
    {
        private readonly HttpClient _httpClient;
        private const string ComponentsUrl = "api/pagecomponents";

        public PageComponentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PageComponent>> GetPageComponentsAsync(int pageId)
        {
            return await _httpClient.GetFromJsonAsync<List<PageComponent>>($"{ComponentsUrl}/page/{pageId}");
        }

        public async Task<PageComponent> GetPageComponentAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PageComponent>($"{ComponentsUrl}/{id}");
        }

        public async Task<PageComponent> CreatePageComponentAsync(PageComponent component)
        {
            var response = await _httpClient.PostAsJsonAsync(ComponentsUrl, component);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PageComponent>();
        }

        public async Task UpdatePageComponentAsync(int id, PageComponent component)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ComponentsUrl}/{id}", component);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePageComponentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ComponentsUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task ReorderPageComponentsAsync(List<ReorderItem> reorderItems)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ComponentsUrl}/reorder", reorderItems);
            response.EnsureSuccessStatusCode();
        }

        public class ReorderItem
        {
            public int Id { get; set; }
            public int Order { get; set; }
        }
    }
}
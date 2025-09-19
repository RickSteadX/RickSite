using System;
using System.Text.Json;

namespace PersonalWebsite.Client.Models
{
    public class PageComponent
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string ComponentType { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string StyleJson { get; set; } = string.Empty;
        public int Order { get; set; }
        public string PropertiesJson { get; set; } = string.Empty;
        
        // Helper method to get properties as dynamic object
        public T GetProperties<T>() where T : class, new()
        {
            if (string.IsNullOrEmpty(PropertiesJson))
                return new T();
                
            return JsonSerializer.Deserialize<T>(PropertiesJson) ?? new T();
        }
        
        // Helper method to set properties from object
        public void SetProperties<T>(T properties) where T : class
        {
            PropertiesJson = JsonSerializer.Serialize(properties);
        }
    }
}
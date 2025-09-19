using System;
using System.Text.Json;

namespace PersonalWebsite.Client.Models
{
    public class PageComponent
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string ComponentType { get; set; }
        public string Content { get; set; }
        public string StyleJson { get; set; }
        public int Order { get; set; }
        public string PropertiesJson { get; set; }
        
        // Helper method to get properties as dynamic object
        public T GetProperties<T>() where T : class, new()
        {
            if (string.IsNullOrEmpty(PropertiesJson))
                return new T();
                
            return JsonSerializer.Deserialize<T>(PropertiesJson);
        }
        
        // Helper method to set properties from object
        public void SetProperties<T>(T properties) where T : class
        {
            PropertiesJson = JsonSerializer.Serialize(properties);
        }
    }
}
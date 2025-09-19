using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PersonalWebsite.Server.Models
{
    public class PageComponent
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int PageId { get; set; }
        
        public Page Page { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ComponentType { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public string StyleJson { get; set; }
        
        public int Order { get; set; }
        
        // Store component properties as JSON
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
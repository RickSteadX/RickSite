using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PersonalWebsite.Client.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int Order { get; set; }
        public bool ShowInNavigation { get; set; }
        public string LayoutJson { get; set; } = string.Empty;
        
        // Navigation property for components
        public List<PageComponent>? Components { get; set; }
        
        // Helper method to get layout as dictionary
        public Dictionary<string, object> GetLayout()
        {
            if (string.IsNullOrEmpty(LayoutJson))
                return new Dictionary<string, object>();
                
            return JsonSerializer.Deserialize<Dictionary<string, object>>(LayoutJson) ?? new Dictionary<string, object>();
        }
        
        // Helper method to set layout from dictionary
        public void SetLayout(Dictionary<string, object> layout)
        {
            LayoutJson = JsonSerializer.Serialize(layout);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PersonalWebsite.Client.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int Order { get; set; }
        public bool ShowInNavigation { get; set; }
        public string LayoutJson { get; set; }
        
        // Helper method to get layout as dictionary
        public Dictionary<string, object> GetLayout()
        {
            if (string.IsNullOrEmpty(LayoutJson))
                return new Dictionary<string, object>();
                
            return JsonSerializer.Deserialize<Dictionary<string, object>>(LayoutJson);
        }
        
        // Helper method to set layout from dictionary
        public void SetLayout(Dictionary<string, object> layout)
        {
            LayoutJson = JsonSerializer.Serialize(layout);
        }
    }
}
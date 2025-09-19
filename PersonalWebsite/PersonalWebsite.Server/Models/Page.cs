using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PersonalWebsite.Server.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Slug { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public bool IsPublished { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        public DateTime? PublishedAt { get; set; }
        
        public int Order { get; set; } = 0;
        
        public bool ShowInNavigation { get; set; } = true;
        
        // Store page layout as JSON
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
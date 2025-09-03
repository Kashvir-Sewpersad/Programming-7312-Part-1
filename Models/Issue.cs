using System;
using System.ComponentModel.DataAnnotations;

namespace Programming_7312_Part_1.Models
{
    public class Issue
    {
        [Required]
        public string? Location { get; set; } // Nullable string

        [Required]
        public string? Category { get; set; } // Nullable string

        [Required]
        public string? Description { get; set; } // Nullable string

        public string? AttachedFilePath { get; set; } // Nullable string

        public DateTime ReportedDate { get; set; }

        public Issue()
        {
            ReportedDate = DateTime.Now;
        }
    }
}

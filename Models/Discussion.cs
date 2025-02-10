using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscussionForm.Models
{
    public class Discussion
    {
        [Key]
        public int DiscussionId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? ImageFilename { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Navigation Property
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

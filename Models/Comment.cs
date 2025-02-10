using System;
using System.ComponentModel.DataAnnotations;

namespace DiscussionForm.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Foreign Key
        public int DiscussionId { get; set; }

        public Discussion Discussion { get; set; }
    }
}

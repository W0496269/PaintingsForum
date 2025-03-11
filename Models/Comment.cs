using System;
using System.ComponentModel.DataAnnotations;

namespace DiscussionForm.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DiscussionForm.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageFilename { get; set; }

        public DateTime CreateDate { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}

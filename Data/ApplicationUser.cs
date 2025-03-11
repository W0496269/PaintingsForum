using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DiscussionForm.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Discussion> Discussions { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

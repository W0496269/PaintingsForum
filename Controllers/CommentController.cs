using Microsoft.AspNetCore.Mvc;
using DiscussionForm.Models;
using DiscussionForm.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DiscussionForm.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int discussionId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            var comment = new Comment
            {
                Content = content,
                CreateDate = DateTime.Now,
                DiscussionId = discussionId,
                ApplicationUserId = user.Id
            };

            _context.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(HomeController.GetDiscussion), "Home", new { id = discussionId });
        }
    }
}

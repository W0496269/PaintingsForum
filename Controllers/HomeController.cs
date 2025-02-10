using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DiscussionForm.Models;
using DiscussionForm.Data;

namespace DiscussionForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the list of discussions
        public IActionResult Index()
        {
            var discussions = _context.Discussions
                .Include(d => d.Comments)
                .OrderByDescending(d => d.CreateDate)
                .ToList();

            return View(discussions);
        }

        // Get a specific discussion by ID and show it along with comments
        public IActionResult GetDiscussion(int id)
        {
            var discussion = _context.Discussions
                .Include(d => d.Comments)
                .Where(d => d.DiscussionId == id)
                .FirstOrDefault();

            if (discussion == null)
            {
                return NotFound();
            }

            // Order comments by CreateDate in descending order
            discussion.Comments = discussion.Comments.OrderByDescending(c => c.CreateDate).ToList();

            return View(discussion);
        }
    }
}
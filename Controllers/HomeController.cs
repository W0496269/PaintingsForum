using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiscussionForm.Models;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussions
                .Include(d => d.User)
                .OrderByDescending(d => d.CreateDate)
                .ToListAsync();
            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussions
                .Include(d => d.Comments)
                    .ThenInclude(c => c.User)
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }
    }
}

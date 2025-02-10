using DiscussionForm.Data;
using DiscussionForm.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DiscussionForm.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comment/Create (For adding a new comment)
        public IActionResult Create(int discussionId)
        {
            // Pass the DiscussionId to the view
            ViewData["DiscussionId"] = discussionId;
            return View();
        }

        // POST: Comment/Create (Submitting the new comment)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string content, int discussionId)
        {
            if (ModelState.IsValid)
            {
                var discussion = await _context.Discussions.FindAsync(discussionId);

                if (discussion != null)
                {
                    var comment = new Comment
                    {
                        Content = content,
                        DiscussionId = discussionId,
                        CreateDate = DateTime.Now
                    };

                    _context.Comments.Add(comment);
                    await _context.SaveChangesAsync();

                    // Redirect back to the GetDiscussion page with the updated comments
                    return RedirectToAction("GetDiscussion", "Home", new { id = discussionId });
                }

                return NotFound();
            }

            // If something fails, redirect back to the GetDiscussion page
            return RedirectToAction("GetDiscussion", "Home", new { id = discussionId });
        }
    }
}
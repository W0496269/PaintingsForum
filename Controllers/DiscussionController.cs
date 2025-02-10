using Microsoft.AspNetCore.Mvc;
using DiscussionForm.Models;
using DiscussionForm.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiscussionForm.Controllers
{
    public class DiscussionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscussionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Discussion/Create
        public IActionResult Create()
        {
            return View();
        }

        // ✅ POST: Discussion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discussion discussion, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (image != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    discussion.ImageFilename = fileName;
                }

                discussion.CreateDate = DateTime.Now;
                _context.Add(discussion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // ✅ GET: Discussion (List Discussions)
        public IActionResult Index()
        {
            var discussions = _context.Discussions.OrderByDescending(d => d.CreateDate).ToList();
            return View(discussions);
        }
    }
}
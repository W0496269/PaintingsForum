using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using DiscussionForm.Models;
using DiscussionForm.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForm.Controllers
{
    [Authorize]
    public class DiscussionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiscussionController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        // Create Discussion
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                discussion.CreateDate = DateTime.Now;
                discussion.ApplicationUserId = user.Id;

                // Handle image upload
                if (discussion.ImageFile != null)
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", discussion.ImageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(stream);
                    }
                    discussion.ImageFilename = discussion.ImageFile.FileName;
                }

                _context.Add(discussion);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(HomeController.Index));
            }
            return View(discussion);
        }

        // Edit Discussion
        public async Task<IActionResult> Edit(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (discussion.ApplicationUserId != user.Id)
            {
                return Forbid();
            }

            return View(discussion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Discussions.Any(d => d.DiscussionId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(HomeController.Index));
            }

            return View(discussion);
        }

        // Delete Discussion
        public async Task<IActionResult> Delete(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (discussion.ApplicationUserId != user.Id)
            {
                return Forbid();
            }

            _context.Discussions.Remove(discussion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}

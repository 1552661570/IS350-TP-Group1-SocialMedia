using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using IS350_TP_Group1_SocialMedia.Data;
using IS350_TP_Group1_SocialMedia.Models;
using System.IO;

namespace IS350_TP_Group1_SocialMedia.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [TempData]
        public string currentUserName { get; set; }
        public bool isThumbed = false;
        public PostsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            TempData["currentUserName"] = "Haoze Xinchen";
            return View(await _context.Post.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.postID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    string guidFileName = null;
                    if (model.picturePath != null)
                    {
                        var fileFomat = Path.GetExtension(model.picturePath.FileName).ToLower();
                        const string fomatFilt = ".gif|.jpg|.jpeg|.png";
                        if (fomatFilt.IndexOf(fileFomat, StringComparison.Ordinal) <= -1)
                            return Content("<script >alert('Please upload JPG,JPEG, PNG, GIF images');window.open('" + Url.Content("/Posts/Create") + "', '_self')</script >", "text/html");
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        guidFileName = Guid.NewGuid().ToString() + fileFomat;
                        string filePath = Path.Combine(uploadsFolder, guidFileName);
                        FileStream fileStream = new FileStream(filePath, FileMode.Create);
                        fileStream.Position = 0;
                        await model.picturePath.CopyToAsync(fileStream);
                        fileStream.Flush();
                    }
                    else
                        return Content("<script >alert('picture path is empty');window.open('" + Url.Content("/Posts/Create") + "', '_self')</script >", "text/html");

                    Post newMsg = new Post
                    {
                        userName = TempData.Peek("currentUserName").ToString(),
                        content = model.content,
                        sendDate = DateTime.Now,
                        picturePath = guidFileName,
                        thumbUpNum = 0
                    };

                    _context.Add(newMsg);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("postID,userName,content,sendDate,picturePath,thumbUpNum")] Post post)
        {
            if (id != post.postID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.postID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.postID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.postID == id);
        }

        public async Task<IActionResult> ThumbUp(int id)
        {
            var post = _context.Post.FirstOrDefault(e => e.postID == id);
            post.thumbUpNum += 1;
            _context.Update(post);
            await _context.SaveChangesAsync();
            isThumbed = true;
            return RedirectToAction(nameof(Index));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Memories.Models;
using Memories.Data;

namespace Memories.Areas.FamilyArea.Controllers
{
    [Area("FamilyArea")]
    //[Authorize]


    public class FamilyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FamilyController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Members.Count() / pageSize);
            return View(await _context.Members.OrderByDescending(p => p.Id)
                .Include(p => p.Family)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync());



        }

        public IActionResult Create()
        {
            ViewBag.Families = new SelectList(_context.Families, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            ViewBag.Families = new SelectList(_context.Families, "Id", "Name", member.CategoryId);

            if (ModelState.IsValid)
            {
                member.Slug = member.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Members.FirstOrDefaultAsync(p => p.Slug == member.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The product already exists.");
                    return View(member);
                }

                string imageName;

                if (member.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/Members");
                    imageName = Guid.NewGuid().ToString() + " " + member.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await member.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    member.Image = imageName;

                }

                _context.Add(member);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been created.";

                return RedirectToAction("Index");

            }

            return View(member);
        }

        public async Task<IActionResult> Edit(long id)
        {

            Member member = await _context.Members.FindAsync(id);
            ViewBag.Families = new SelectList(_context.Families, "Id", "Name");

            return View(member);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Member member)
        {
            ViewBag.Families = new SelectList(_context.Families, "Id", "Name", member.CategoryId);

            if (ModelState.IsValid)
            {
                member.Slug = member.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Members.FirstOrDefaultAsync(p => p.Slug == member.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The product already exists.");
                    return View(member);
                }

                string imageName;

                if (member.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/Products");
                    imageName = Guid.NewGuid().ToString() + " " + member.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await member.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    member.Image = imageName;

                }

                _context.Update(member);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been edited.";

            }

            return View(member);
        }

        public async Task<IActionResult> Delete(long id)
        {

            Member member = await _context.Members.FindAsync(id);

            if (!string.Equals(member.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "/Media/Products");
                string oldImagePath = Path.Combine(uploadsDir, member.Image);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                };

            };
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            TempData["Success"] = "The product has been deleted.";

            return RedirectToAction("Index");
        }
    }
}
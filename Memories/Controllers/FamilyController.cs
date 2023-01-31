using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Memories.Data;
using Memories.Models;

namespace Memories.Controllers
{
    public class FamilyController : Controller { 
    
        private readonly ApplicationDbContext _context;
        public FamilyController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = categorySlug;
            if (categorySlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Members.Count() / pageSize);

                return View(await _context.Members.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
            }

            Family family = await _context.Families.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();

            if (family == null) return RedirectToAction("index");

            var productsbyCategory = _context.Members.Where(p => p.CategoryId == family.Id);
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsbyCategory.Count() / pageSize);

            return View(await productsbyCategory.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());

        }
    }
}
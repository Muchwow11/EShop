using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTagsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //var data = _db.Categories.ToList();
            return View(_db.ProductTags.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTags productTags)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTags.Add(productTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTags productTags)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Categories categories)
        {
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTags productTags)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != productTags.Id)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTag);
        }
    }
}

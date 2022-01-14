using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.Categories.ToList();
            return View(_db.Categories.ToList());
        }


        //GET Create Action Method
        public ActionResult Create()
        {
            return View();
        }
        //GET Edit Action Methot
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST Edit Action Method
        public async Task<IActionResult> Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(categories);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                _db.Update(categories);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Categories categories)
        {
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Categories categories)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != categories.Id)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

    }
}

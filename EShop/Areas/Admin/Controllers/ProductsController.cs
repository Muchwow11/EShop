using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _he;
        public ProductsController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.Categories).Include(v => v.ProductTags).ToList());
        }
        //GET Create method
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories.ToList(), "Id", "Category");
            ViewData["TagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "Name");
            return View();
        }
        //POST Create method
        [HttpPost]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "Images/nopng.jpg";
                }
                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
        //GET Edit method
        public ActionResult Edit(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories.ToList(), "Id", "Category");
            ViewData["TagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Categories).Include(c => c.ProductTags)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        //POST Edit method 
        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/nopng.jpg";
                }
                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(products);
        }
        //GET Delete method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.ProductTags).Include(c => c.Categories).Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //POST Delete method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //GET Details method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = _db.Products.Include(x => x.Categories).Include(x => x.ProductTags)
                .FirstOrDefault(x => x.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
    }
}

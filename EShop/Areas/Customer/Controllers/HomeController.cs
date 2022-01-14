using EShop.Data;
using EShop.Models;
using EShop.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View(_db.Products.Include(x => x.Categories).Include(x => x.ProductTags).ToList());
        }

        // GET Product Info
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Categories).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //POST Product Info
        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductDetails(int? id)
        {
            List<Products> products = new List<Products>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Categories).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }

            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }

        //GET Remove action methdo
        [ActionName("Remove")]
        public IActionResult RemoveItem(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET Cart method
        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products==null)
            {
                products = new List<Products>();
            }
            return View(products);
        }

    }
}

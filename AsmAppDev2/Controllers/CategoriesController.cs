using AsmAppDev2.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AsmAppDev2.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Categories
        [HttpGet]
        public ActionResult Index(string search)
        {
            var category = _context.Categories.ToList();

            if (!String.IsNullOrEmpty(search))
            {
                category = category.FindAll(s => s.Name.Contains(search));
            }

            return View(category);
        }

        //GET: Create 
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var check = _context.Categories.Any(
              c => c.Name.Contains(category.Name));

            if (check)
            {
                ModelState.AddModelError("Name", "Category Name Already Exists.");
                return View();
            }

            var create = new Category
            {
                Name = category.Name,
                Description = category.Description,
            };

            _context.Categories.Add(create);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET: Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.ID == id);

            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            return View(categoryInDb);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var categoryInDb = _context.Categories.SingleOrDefault(c => c.ID == category.ID);

            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            var check = _context.Categories.Any(
              c => c.Name.Contains(category.Name));

            if (check)
            {
                ModelState.AddModelError("Name", "Category Name Already Exists.");
                return View();
            }
            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.ID == id);

            if (categoryInDb == null)
            {
                return HttpNotFound();
            }

            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
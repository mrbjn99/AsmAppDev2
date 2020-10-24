using AsmAppDev2.Models;
using AsmAppDev2.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace AsmAppDev2.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Courses
        [HttpGet]
        public ActionResult Index(string search)
        {
            var courses = _context.Courses.Include(c => c.Category);

            if (!String.IsNullOrEmpty(search))
            {
                courses = courses.Where(
                    s => s.Name.Contains(search) ||
                    s.Category.Name.Contains(search));
            }
            return View(courses);
        }

        //GET: Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseCategoryViewModel
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var check = _context.Courses.Any(
                c => c.Name.Contains(course.Name));

            if (check)
            {
                ModelState.AddModelError("Name", "Course Name Already Exists.");
                return RedirectToAction("Index");
            }

            var create = new Course
            {
                Name = course.Name,
                Description = course.Description,
                CategoryID = course.CategoryID,
            };
            _context.Courses.Add(create);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.ID == id);
            if (courseInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CourseCategoryViewModel
            {
                Course = courseInDb,
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var courseInDb = _context.Courses.SingleOrDefault(co => co.ID == course.ID);
            if (courseInDb == null)
            {
                return HttpNotFound();
            }
            var check = _context.Courses.Any(
                c => c.Name.Contains(course.Name));
            if (check)
            {
                ModelState.AddModelError("Name", "Course Name Already Exists.");
                return View();
            }
            courseInDb.Name = course.Name;
            courseInDb.Description = course.Description;
            courseInDb.CategoryID = course.CategoryID;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.ID == id);

            if (courseInDb == null)
            {
                return HttpNotFound();
            }
            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
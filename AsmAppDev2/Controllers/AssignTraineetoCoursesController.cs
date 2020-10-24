using AsmAppDev2.Models;
using AsmAppDev2.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AsmAppDev2.Controllers
{
    public class AssignTraineetoCoursesController : Controller
    {
        private ApplicationDbContext _context;
        public AssignTraineetoCoursesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        // GET: AssignTraineetoCourses
        public ActionResult Index(string search)
        {
            var traineeCourse = _context.AssignTraineetoCourses
                                     .Include(te => te.Course)
                                     .Include(te => te.Trainee);

            if (!String.IsNullOrEmpty(search))
            {
                traineeCourse = traineeCourse.Where(
                        s => s.Trainee.UserName.Contains(search) ||
                        s.Trainee.Email.Contains(search));
            }

            return View(traineeCourse);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Get Account Trainee
            var roleInDb = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();

            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleInDb.Id)).ToList();

            //Get Course
            var courses = _context.Courses.ToList();

            var viewModel = new AssignTraineetoCourseViewModel
            {
                Courses = courses,
                Trainees = users,
                AssignTraineetoCourse = new AssignTraineetoCourse()
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(AssignTraineetoCourse assignTraineetoCourse)
        {
            var assign = new AssignTraineetoCourse
            {
                TraineeID = assignTraineetoCourse.TraineeID,
                CourseID = assignTraineetoCourse.CourseID
            };

            _context.AssignTraineetoCourses.Add(assign);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var assignInDb = _context.AssignTraineetoCourses.SingleOrDefault(a => a.ID == id);
            if (assignInDb == null)
            {
                return HttpNotFound();
            }

            _context.AssignTraineetoCourses.Remove(assignInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
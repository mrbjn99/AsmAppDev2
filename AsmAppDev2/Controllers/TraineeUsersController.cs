using AsmAppDev2.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace AsmAppDev2.Controllers
{
    public class TraineeUsersController : Controller
    {
        private ApplicationDbContext _context;
        public TraineeUsersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Trainees
        [HttpGet]
        public ActionResult Index(string search)
        {
            var trainee = _context.TraineeUsers.Include(tp => tp.Trainees);

            if (!String.IsNullOrEmpty(search))
            {
                trainee = trainee.Where(
                        s => s.Trainee.UserName.Contains(search) ||
                        s.Trainee.Email.Contains(search));
            }
            return View(trainee);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Get Account Trainee
            var userInDb = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(userInDb.Id)).ToList();
            var traineeUser = _context.TraineeUsers.ToList();

            var traineeInfo = new TraineeUser
            {
                Trainees = users,

            };
            return View(traineeInfo);
        }

        [HttpPost]
        public ActionResult Create(TraineeUser traineeUser)
        {
            var traineeInfo = new TraineeUser
            {
                TraineeID = traineeUser.TraineeID,
                Full_Name = traineeUser.Full_Name,
                Education = traineeUser.Education,
                Programming_Language = traineeUser.Programming_Language,
                Experience_Details = traineeUser.Experience_Details,
                Department = traineeUser.Department,
                Phone = traineeUser.Phone
            };
            _context.TraineeUsers.Add(traineeInfo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var traineeInDb = _context.TraineeUsers.SingleOrDefault(te => te.ID == id);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }
            _context.TraineeUsers.Remove(traineeInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var traineeInDb = _context.TraineeUsers.SingleOrDefault(te => te.ID == id);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }
            return View(traineeInDb);
        }

        [HttpPost]
        public ActionResult Edit(TraineeUser traineeUser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var traineeInDb = _context.TraineeUsers.SingleOrDefault(te => te.ID == traineeUser.ID);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }

            traineeInDb.TraineeID = traineeUser.TraineeID;
            traineeInDb.Full_Name = traineeUser.Full_Name;
            traineeInDb.Education = traineeUser.Education;
            traineeInDb.Programming_Language = traineeUser.Programming_Language;
            traineeInDb.Experience_Details = traineeUser.Experience_Details;
            traineeInDb.Department = traineeUser.Department;
            traineeInDb.Phone = traineeUser.Phone;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //[Authorize(Roles = "Trainee")]
        //public ActionResult Mine()
        //{
        //	var userId = User.Identity.GetUserId();

        //	var traineeProfiles = _context.Trainees
        //		.Where(c => c.TraineeID == userId)
        //		.Include(c => c.Trainees)
        //		.ToList();

        //	return View(traineeProfiles);

    }
}
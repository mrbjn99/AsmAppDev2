using AsmAppDev2.Models;
using AsmAppDev2.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AsmAppDev2.Controllers
{
    public class AssignTrainertoTopicsController : Controller
    {
        private ApplicationDbContext _context;

        public AssignTrainertoTopicsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: AssignTrainertoTopics
        [HttpGet]
        public ActionResult Index(string search)
        {
            var trainerTopic = _context.AssignTrainertoTopics
                                     .Include(tr => tr.Topic)
                                     .Include(tr => tr.Trainer);

            if (!String.IsNullOrEmpty(search))
            {
                trainerTopic = trainerTopic.Where(
                        s => s.Trainer.UserName.Contains(search) ||
                        s.Trainer.Email.Contains(search));
            }

            return View(trainerTopic);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Get Account Trainer
            var roleInDb = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();

            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleInDb.Id)).ToList();

            //Get Topic
            var topics = _context.Topics.ToList();

            var viewModel = new AssignTrainertoTopicViewModel
            {
                Topics = topics,
                Trainers = users,
                AssignTrainertoTopic = new AssignTrainertoTopic()
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(AssignTrainertoTopic assignTrainertoTopic)
        {
            var assign = new AssignTrainertoTopic
            {
                TrainerID = assignTrainertoTopic.TrainerID,
                TopicID = assignTrainertoTopic.TopicID
            };

            _context.AssignTrainertoTopics.Add(assign);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var assignInDb = _context.AssignTrainertoTopics.SingleOrDefault(a => a.ID == id);
            if (assignInDb == null)
            {
                return HttpNotFound();
            }

            _context.AssignTrainertoTopics.Remove(assignInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
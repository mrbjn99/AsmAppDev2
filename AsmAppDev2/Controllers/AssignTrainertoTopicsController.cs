using AsmAppDev2.Models;
using AsmAppDev2.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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
		public ActionResult Index()
		{
			if (User.IsInRole("Staff"))
			{
				var assign = _context.AssignTrainertoTopics.Include(a => a.Topic).Include(a => a.Trainer).ToList();
				return View(assign);
			}
			if (User.IsInRole("Trainer"))
			{
				var trainerId = User.Identity.GetUserId();
				var trainerVM = _context.AssignTrainertoTopics.Where(te => te.TrainerID == trainerId).Include(te => te.Topic).ToList();
				return View(trainerVM);
			}
			return View("Login");
		}

		//GET: Trainee Course
		public ActionResult Create()
		{
			var trainer = (from tn in _context.Roles where tn.Name.Contains("Trainer") select tn).FirstOrDefault();
			var trainerUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(trainer.Id)).ToList();

			var topic = _context.Topics.ToList();
			var trainertopicVM = new AssignTrainertoTopicViewModels()
			{
				Topics = topic,
				Trainers = trainerUser,
				assignTrainertoTopics = new AssignTrainertoTopic()
			};
			return View(trainertopicVM);
		}

		//Post: Trainee Course
		[HttpPost]
		public ActionResult Create(AssignTrainertoTopicViewModels model)
		{
			var trainer = (from tn in _context.Roles where tn.Name.Contains("Trainer") select tn).FirstOrDefault();
			var trainerUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(trainer.Id)).ToList();

			var topic = _context.Topics.ToList();

			if (ModelState.IsValid)
			{
				_context.AssignTrainertoTopics.Add(model.assignTrainertoTopics);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}

			var trainertopicVM = new AssignTrainertoTopicViewModels()
			{
				Topics = topic,
				Trainers = trainerUser,
				assignTrainertoTopics = new AssignTrainertoTopic()
			};
			return View(trainertopicVM);
		}
	}
}
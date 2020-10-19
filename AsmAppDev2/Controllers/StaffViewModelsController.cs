using AsmAppDev2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsmAppDev2.ViewModels;

namespace AsmAppDev2.Controllers
{
	public class StaffViewModelsController : Controller
	{
		ApplicationDbContext _context;
		public StaffViewModelsController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: StaffViewModels
		public ActionResult Index()
		{
			var traineeRole = (from te in _context.Roles where te.Name.Contains("Trainee") select te).FirstOrDefault();
			var traineeUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(traineeRole.Id)).ToList();
			var traineeUserVM = traineeUser.Select(user => new StaffViewModels
			{
				UserName = user.UserName,
				Email = user.Email,
				RoleName = "Trainee",
				UserID = user.Id
			}).ToList();

			var trainerRole = (from tn in _context.Roles where tn.Name.Contains("Trainer") select tn).FirstOrDefault();
			var trainerUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(trainerRole.Id)).ToList();
			var trainerUserVM = trainerUser.Select(user => new StaffViewModels
			{
				UserName = user.UserName,
				Email = user.Email,
				RoleName = "Trainer",
				UserID = user.Id
			}).ToList();
			var staff = new StaffViewModels { Trainee = traineeUserVM, Trainer = trainerUserVM };
			return View(staff);
		}

		[HttpGet]
		public ActionResult Edit (string id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}
			var edit = _context.Users.Find(id);
			if (edit == null)
			{
				return HttpNotFound();
			}
			return View(edit);
		}
		[HttpPost]
		public ActionResult Edit (StaffViewModels staffViewModels)
		{
			var userInDB = _context.Users.Find(staffViewModels.ID);
			if (userInDB == null)
			{
				return View(staffViewModels);
			}
			if (ModelState.IsValid)
			{
				userInDB.UserName = staffViewModels.UserName;
				userInDB.Email = staffViewModels.Email;

				_context.Users.AddOrUpdate(userInDB);
				_context.SaveChanges();
				return RedirectToAction("Index");

			}
			return View(staffViewModels);
		}

		public ActionResult Delete(ApplicationUser user)
		{
			var userInDB = _context.Users.Find(user.Id);
			if (userInDB == null)
			{
				return View(user);
			}
			if (ModelState.IsValid)
			{
				userInDB.UserName = user.UserName;
				userInDB.Email = user.Email;
				_context.Users.Remove(userInDB);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}
	}
}
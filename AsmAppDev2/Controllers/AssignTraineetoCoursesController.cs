using AsmAppDev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using AsmAppDev2.ViewModels;

namespace AsmAppDev2.Controllers
{
	public class AssignTraineetoCoursesController : Controller
	{
		private ApplicationDbContext _context;
		public AssignTraineetoCoursesController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: AssignTraineetoCourses
		public ActionResult Index()
		{
			if (User.IsInRole("Staff"))
			{
				var assign = _context.AssignTraineetoCourses.Include(a => a.Course).Include(a => a.Trainee).ToList();
				//Đối với truy vấn truy xuất dữ liệu liên quan, trả về danh sách hoặc đối tượng, chẳng hạn như Danh sách hoặc Đơn.
				return View(assign);
			}
			if (User.IsInRole("Trainee"))
			{
				var traineeId = User.Identity.GetUserId();
				var traineeVM = _context.AssignTraineetoCourses.Where(te => te.TraineeID == traineeId).Include(te => te.Course).ToList();
				return View(traineeVM);
			}
			return View("Login");
		}

		//GET: Assign Trainee Course
		public ActionResult Create()
		{
			var trainee = (from te in _context.Roles where te.Name.Contains("Trainee") select te).FirstOrDefault();
			var traineeUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(trainee.Id)).ToList();

			var course = _context.Courses.ToList();
			var traineecourseVM = new AssignTraineetoCourseViewModels()
			{
				Courses = course,
				Trainees = traineeUser,
				AssignTraineetoCourses = new AssignTraineetoCourse()
			};
			return View(traineecourseVM);
		}
		
		//Post: Assign Trainee Course
		[HttpPost]
		public ActionResult Create(AssignTraineetoCourseViewModels assign)
		{
			var trainee = (from te in _context.Roles where te.Name.Contains("Trainee") select te).FirstOrDefault();
			var traineeUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(trainee.Id)).ToList();

			var course = _context.Courses.ToList();

			if (ModelState.IsValid)
			{
				_context.AssignTraineetoCourses.Add(assign.AssignTraineetoCourses);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}

			var traineecourseVM = new AssignTraineetoCourseViewModels()
			{
				Courses = course,
				Trainees = traineeUser,
				AssignTraineetoCourses = new AssignTraineetoCourse()
			};
			return View(traineecourseVM);
		}

		//public ActionResult Delete (AssignTraineetoCourse assign)
		//{
		//	var assignInDb = _context.AssignTraineetoCourses.Find(assign.Trainee);
		//	if (assignInDb == null)
		//	{
		//		return View(assign);
		//	}
		//	if (ModelState.IsValid)
		//	{
		//		assignInDb.Course = assign.Course;
		//		assignInDb.Trainee = assign.Trainee;
		//		_context.AssignTraineetoCourses.Remove(assignInDb);
		//		_context.SaveChanges();
		//		return RedirectToAction("Index");
		//	}
		////	return View(assign);
		//}
	}
}
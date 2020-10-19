using AsmAppDev2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AsmAppDev2.Controllers
{
	public class CoursesController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Courses
		public ActionResult Index(string searchCourse)
		{
			var courses = db.Courses
			.Include(c => c.Category)
			.Include(c => c.Topic);
			if (!String.IsNullOrEmpty(searchCourse))
			{
				courses = courses.Where(c =>
				c.Name_Course.Contains(searchCourse) |
				c.Category.Name_Category.Contains(searchCourse) |
				c.Topic.Name_Topic.Contains(searchCourse));
			}	
			return View(courses);
		}

		// GET: View Course Details
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var course = db.Courses.Find(id);
			if (course == null)
			{
				return HttpNotFound();
			}
			return View(course);
		}

		// GET: Create Course
		public ActionResult Create()
		{
			ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name_Category");
			ViewBag.TopicID = new SelectList(db.Topics, "ID", "Name_Topic");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Course course)
		{
			if (ModelState.IsValid)
			{
				db.Courses.Add(course);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name_Category", course.CategoryID);
			ViewBag.TopicId = new SelectList(db.Topics, "ID", "Name_Topic", course.TopicID);
			return View(course);
		}

		// GET: Edit Course
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Course course = db.Courses.Find(id);
			if (course == null)
			{
				return HttpNotFound();
			}
			ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name_Category", course.CategoryID);
			ViewBag.TopicId = new SelectList(db.Topics, "ID", "Name_Topic", course.TopicID);
			return View(course);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Course course)
		{
			if (ModelState.IsValid)
			{
				db.Entry(course).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name_Category", course.CategoryID);
			ViewBag.TopicId = new SelectList(db.Topics, "ID", "Name_Topic", course.TopicID);
			return View(course);
		}

		// GET: Delete Course
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Course course = db.Courses.Find(id);
			if (course == null)
			{
				return HttpNotFound();
			}
			return View(course);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Course course = db.Courses.Find(id);
			db.Courses.Remove(course);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
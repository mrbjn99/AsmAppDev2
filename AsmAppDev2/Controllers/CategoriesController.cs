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
	public class CategoriesController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Categories
		public ActionResult Index()
		{
			return View(db.Categories.ToList());
		}

		//GET: View Categories Details
		public ActionResult View_Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Category category = db.Categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// GET: Create Category
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
				return RedirectToAction("Create");
			}
			db.Categories.Add(category);
			db.SaveChanges();
			return RedirectToAction("Index");
		}


		// GET: Edit Category
		[HttpGet]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Category category = db.Categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				db.Entry(category).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(category);
		}

		// GET: Delete Category
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var categoryInDb = db.Categories.SingleOrDefault(ca => ca.ID == id);
			if (categoryInDb == null)
			{
				return HttpNotFound();
			}
			db.Categories.Remove(categoryInDb);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
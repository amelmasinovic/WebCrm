﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebCrm.App_Data;

namespace WebCrm.Controllers
{
	[Authorize]
	public class TaskListController : Controller
	{
		private WebCrmModelContainer db = new WebCrmModelContainer();
		private ApplicationUserManager UserManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }

		// GET: TaskList
		public ActionResult Index()
		{
			return View(db.TaskSet.ToList());
		}

		// GET: TaskList/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Task task = db.TaskSet.Find(id);
			if (task == null)
			{
				return HttpNotFound();
			}

			task.CreateUserObject = UserManager.FindById(task.CreateUser);

			return View(task);
		}

		// GET: TaskList/Create
		public ActionResult Create()
		{
			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;
			var personList = GetPersonSelectList();
			ViewBag.PersonList = personList;

			return View();
		}
		// POST: TaskList/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,Description,Date,CompanyId,PersonId")] Task task)
		{
			if (ModelState.IsValid)
			{
				task.CreateUser = User.Identity.GetUserId();

				db.TaskSet.Add(task);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;
			var personList = GetPersonSelectList();
			ViewBag.PersonList = personList;

			return View(task);
		}

		// GET: TaskList/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Task task = db.TaskSet.Find(id);
			if (task == null)
			{
				return HttpNotFound();
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;
			var personList = GetPersonSelectList();
			ViewBag.PersonList = personList;

			return View(task);
		}

		// POST: TaskList/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,Description,Date,CreateUser")] Task task)
		{
			if (ModelState.IsValid)
			{
				db.Entry(task).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(task);
		}

		// GET: TaskList/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Task task = db.TaskSet.Find(id);
			if (task == null)
			{
				return HttpNotFound();
			}
			return View(task);
		}

		// POST: TaskList/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Task task = db.TaskSet.Find(id);
			db.TaskSet.Remove(task);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
		private List<SelectListItem> GetCompanySelectList()
		{
			var companies = db.CompanySet.ToList();
			var companyList = new List<SelectListItem>();
			foreach (var company in companies)
			{
				companyList.Add(new SelectListItem
				{
					Text = company.Name,
					Value = company.Id.ToString()
				});
			}
			return companyList;
		}
		private object GetPersonSelectList()
		{
			var personSet = db.PersonSet.ToList();
			var personList = new List<SelectListItem>();
			foreach (var person in personSet)
			{
				personList.Add(new SelectListItem
				{
					Text = person.Forename + " " + person.Surname,
					Value = person.Id.ToString()
				});
			}
			return personList;
		}
	}
}

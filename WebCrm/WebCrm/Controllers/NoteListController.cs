using System;
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
	public class NoteListController : Controller
	{
		private WebCrmModelContainer db = new WebCrmModelContainer();
		private ApplicationUserManager UserManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }

		// GET: NoteList
		public ActionResult Index()
		{
			var noteSet = db.NoteSet.Include(n => n.Company).Include(n => n.Person).Include(n => n.Task);
			return View(noteSet.ToList());
		}

		// GET: NoteList/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Note note = db.NoteSet.Find(id);
			if (note == null)
			{
				return HttpNotFound();
			}

			note.CreateUserObject = UserManager.FindById(note.CreateUser);

			return View(note);
		}

		// GET: NoteList/Create
		public ActionResult Create()
		{
			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;
			var personList = GetPersonSelectList();
			ViewBag.PersonList = personList;

			return View();
		}

		// POST: NoteList/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,Description,CreateUser,CreateDate,CompanyId,PersonId,TaskId")] Note note)
		{
			if (ModelState.IsValid)
			{
				note.CreateUser = User.Identity.GetUserId();

				db.NoteSet.Add(note);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;
			var personList = GetPersonSelectList();
			ViewBag.PersonList = personList;

			return View(note);
		}

		// GET: NoteList/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Note note = db.NoteSet.Find(id);
			if (note == null)
			{
				return HttpNotFound();
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;
			var personList = GetPersonSelectList();
			ViewBag.PersonList = personList;

			return View(note);
		}

		// POST: NoteList/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,Description,CreateUser,CreateDate,CompanyId,PersonId,TaskId")] Note note)
		{
			if (ModelState.IsValid)
			{
				db.Entry(note).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;
			var personList = GetPersonSelectList();
			ViewBag.PersonList = personList;

			return View(note);
		}

		// GET: NoteList/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Note note = db.NoteSet.Find(id);
			if (note == null)
			{
				return HttpNotFound();
			}
			return View(note);
		}

		// POST: NoteList/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Note note = db.NoteSet.Find(id);
			db.NoteSet.Remove(note);
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

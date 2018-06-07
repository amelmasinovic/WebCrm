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
using WebCrm.Services;

namespace WebCrm.Controllers
{
	[Authorize]
	public class PersonListController : Controller
	{
		private WebCrmModelContainer db = new WebCrmModelContainer();
		private ApplicationUserManager UserManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }
		private NoteService noteService = new NoteService();

		// GET: PersonList
		public ActionResult Index()
		{
			var personSet = db.PersonSet.ToList();

			foreach (var person in personSet)
			{
				person.CreateUserObject = UserManager.FindById(person.CreateUser);
			}

			return View(personSet);
		}

		// GET: PersonList/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Person person = db.PersonSet.Find(id);
			if (person == null)
			{
				return HttpNotFound();
			}

			person.CreateUserObject = UserManager.FindById(person.CreateUser);

			return View(person);
		}

		// GET: PersonList/Create
		public ActionResult Create()
		{
			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;

			return View();
		}
		// POST: PersonList/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Forename,Surname,Email,Phone,CompanyId")] Person person)
		{
			if (ModelState.IsValid)
			{
				person.CreateUser = User.Identity.GetUserId();

				noteService.CreateNote(CrudOperation.Create, UserManager.FindById(person.CreateUser), person, null, null);

				db.PersonSet.Add(person);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;

			return View(person);
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
		// GET: PersonList/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Person person = db.PersonSet.Find(id);
			if (person == null)
			{
				return HttpNotFound();
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;

			return View(person);
		}

		// POST: PersonList/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Forename,Surname,Email,Phone,CreateUser,CompanyId")] Person person)
		{
			if (ModelState.IsValid)
			{
				noteService.CreateNote(CrudOperation.Update, UserManager.FindById(person.CreateUser), person, null, null);

				db.Entry(person).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			var companyList = GetCompanySelectList();
			ViewBag.CompanyList = companyList;

			return View(person);
		}

		// GET: PersonList/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Person person = db.PersonSet.Find(id);
			if (person == null)
			{
				return HttpNotFound();
			}
			return View(person);
		}

		// POST: PersonList/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Person person = db.PersonSet.Find(id);

			noteService.CreateNote(CrudOperation.Delete, UserManager.FindById(person?.CreateUser), person, null, null);

			db.PersonSet.Remove(person);
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
	}
}

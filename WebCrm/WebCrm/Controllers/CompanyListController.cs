using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCrm.App_Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebCrm.Services;

namespace WebCrm.Controllers
{
	[Authorize]
	public class CompanyListController : Controller
	{
		private WebCrmModelContainer db = new WebCrmModelContainer();
		private ApplicationUserManager UserManager { get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); } }
		private NoteService noteService = new NoteService();

		// GET: CompanyList
		public ActionResult Index()
		{
			var companies = db.CompanySet.ToList();
			foreach (var company in companies)
			{
				company.CreateUserObject = UserManager.FindById(company.CreateUser);
			}

			return View(companies);
		}

		// GET: CompanyList/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Company company = db.CompanySet.Find(id);
			if (company == null)
			{
				return HttpNotFound();
			}

			company.CreateUserObject = UserManager.FindById(company.CreateUser);

			return View(company);
		}

		// GET: CompanyList/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CompanyList/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,Zip,City,Country,CreateUser")] Company company)
		{
			if (ModelState.IsValid)
			{
				company.CreateUser = User.Identity.GetUserId();
				try
				{
					noteService.CreateNote(db, CrudOperation.Create, UserManager.FindById(company.CreateUser), null, company);

					db.CompanySet.Add(company);
					db.SaveChanges();
				}
				catch (DbEntityValidationException e)
				{
					foreach (var eve in e.EntityValidationErrors)
					{
						Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
								eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
									ve.PropertyName, ve.ErrorMessage);
						}
					}
				}

				return RedirectToAction("Index");
			}

			return View(company);
		}

		// GET: CompanyList/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Company company = db.CompanySet.Find(id);
			if (company == null)
			{
				return HttpNotFound();
			}
			return View(company);
		}

		// POST: CompanyList/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,Zip,City,Country,CreateUser")] Company company)
		{
			if (ModelState.IsValid)
			{
				noteService.CreateNote(db, CrudOperation.Update, UserManager.FindById(company.CreateUser), null, company);

				db.Entry(company).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(company);
		}

		// GET: CompanyList/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Company company = db.CompanySet.Find(id);
			if (company == null)
			{
				return HttpNotFound();
			}
			return View(company);
		}

		// POST: CompanyList/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Company company = db.CompanySet.Find(id);

			noteService.CreateNote(db, CrudOperation.Delete, UserManager.FindById(company?.CreateUser), null, company);

			db.CompanySet.Remove(company);
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

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
			return View(db.NoteSet.ToList());
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
			return View();
		}

		// POST: NoteList/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,Description,CreateUser")] Note note)
		{
			if (ModelState.IsValid)
			{
				db.NoteSet.Add(note);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

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
			return View(note);
		}

		// POST: NoteList/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,Description,CreateUser")] Note note)
		{
			if (ModelState.IsValid)
			{
				db.Entry(note).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
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
	}
}

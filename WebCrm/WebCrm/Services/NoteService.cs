using WebCrm.App_Data;
using WebCrm.Models;

namespace WebCrm.Services
{
	public class NoteService
	{
		private WebCrmModelContainer db = new WebCrmModelContainer();

		public void CreateNote(CrudOperation crudOperation, ApplicationUser createUser, Person person = null, Company company = null, Task task = null)
		{
			var note = new Note();
			if (crudOperation == CrudOperation.Create)
			{
				note.Name = "Novi unos od korisnika " + createUser.UserName;
			}
			if (crudOperation == CrudOperation.Create)
			{
				note.Name = "Promjena unosa od korisnika " + createUser.UserName;
			}
			if (crudOperation == CrudOperation.Create)
			{
				note.Name = "Brisanje unosa od korisnika " + createUser.UserName;
			}
			note.Person = person;
			note.Company = company;
			note.Task = task;

			db.NoteSet.Add(note);
			db.SaveChanges();
		}
	}

	public enum CrudOperation
	{
		Create, Update, Delete
	}
}
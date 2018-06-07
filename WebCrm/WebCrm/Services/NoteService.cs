using System.Linq;
using WebCrm.App_Data;
using WebCrm.Models;

namespace WebCrm.Services
{
	public class NoteService
	{

		public void CreateNote(WebCrmModelContainer db, CrudOperation crudOperation, ApplicationUser createUser, Person person = null, Company company = null, Task task = null)
		{
			var note = new Note();

			if (crudOperation == CrudOperation.Create)
			{
				note.Name = "Novi unos od korisnika  " + createUser.UserName;
				note.Person = person;
				note.Company = company;
				note.Task = task;
			}

			if (crudOperation == CrudOperation.Update)
			{
				note.Name = "Promjena unosa od korisnika " + createUser.UserName;
				note.Person = person;
				note.Company = company;
				note.Task = task;
			}

			if (crudOperation == CrudOperation.Delete)
			{
				if (company != null)
				{
					note.Name = "Firma \"" + company.Name + "\" izbrisana od korisnika " + createUser.UserName;
					foreach (var tempNote in db.NoteSet.Where(x => x.CompanyId == company.Id))
					{
						db.NoteSet.Remove(tempNote);
					}
				}
				if (person != null)
				{
					note.Name = "Osoba \"" + person.Forename + " " + person.Surname + "\" izbrisana od korisnika  " + createUser.UserName;
					foreach (var tempNote in db.NoteSet.Where(x => x.PersonId == person.Id))
					{
						db.NoteSet.Remove(tempNote);
					}
				}
				if (task != null)
				{
					note.Name = "Zadatak \"" + task.Name + "\" izbrisan od korisnika " + createUser.UserName;
					foreach (var tempNote in db.NoteSet.Where(x => x.TaskId == task.Id))
					{
						db.NoteSet.Remove(tempNote);
					}
				}
			}


			db.NoteSet.Add(note);
		}
	}

	public enum CrudOperation
	{
		Create, Update, Delete
	}
}
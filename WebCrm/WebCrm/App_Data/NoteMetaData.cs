using System;
using System.ComponentModel.DataAnnotations;
using WebCrm.Models;

namespace WebCrm.App_Data
{
	[MetadataType(typeof(NoteMetaData))]
	public partial class Note
	{
		public ApplicationUser CreateUserObject { get; set; }
	}

	class NoteMetaData
	{
		[Display(Name = "Naziv")]
		[Required(ErrorMessage = "Obavezan unos")]
		public string Name { get; set; }
		[Display(Name = "Opis")]
		public string Description { get; set; }
		[Display(Name = "Korisnik koje je napravio unos")]
		public string CreateUser { get; set; }
		[Display(Name = "Firma")]
		public Nullable<int> CompanyId { get; set; }
		[Display(Name = "Osoba")]
		public Nullable<int> PersonId { get; set; }
		[Display(Name = "Zadatak")]
		public Nullable<int> TaskId { get; set; }

		[Display(Name = "Firma")]
		public virtual Company Company { get; set; }
		[Display(Name = "Osoba")]
		public virtual Person Person { get; set; }
		[Display(Name = "Zadatak")]
		public virtual Task Task { get; set; }
	}
}
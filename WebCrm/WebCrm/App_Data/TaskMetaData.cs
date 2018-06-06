using System;
using System.ComponentModel.DataAnnotations;
using WebCrm.Models;

namespace WebCrm.App_Data
{
	[MetadataType(typeof(TaskMetaData))]
	public partial class Task
	{
		public ApplicationUser CreateUserObject { get; set; }
	}

	class TaskMetaData
	{
		[Display(Name = "Naziv")]
		[Required(ErrorMessage = "Obavezan unos")]
		public string Name { get; set; }
		[Display(Name = "Opis")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[Display(Name = "Firma")]
		public int CompanyId { get; set; }
		[Display(Name = "Osoba")]
		public int PersonId { get; set; }

		[Display(Name = "Firma")]
		public virtual Company Company { get; set; }
		[Display(Name = "Osoba")]
		public virtual Person Person { get; set; }
	}
}
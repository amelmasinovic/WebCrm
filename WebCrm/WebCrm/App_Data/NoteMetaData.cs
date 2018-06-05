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
	}
}
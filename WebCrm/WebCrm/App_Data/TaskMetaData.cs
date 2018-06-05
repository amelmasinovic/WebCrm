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
		public string Description { get; set; }
	}
}
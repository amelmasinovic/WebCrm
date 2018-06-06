using System.Collections.Generic;
using WebCrm.App_Data;

namespace WebCrm.Models
{
	public class DashboardViewModel
	{
		public List<Note> Notes { get; set; }
		public List<Task> Tasks { get; set; }

	}
}
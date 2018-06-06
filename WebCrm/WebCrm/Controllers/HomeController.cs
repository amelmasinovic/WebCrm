using System.Linq;
using System.Web.Mvc;
using WebCrm.App_Data;
using WebCrm.Models;

namespace WebCrm.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private WebCrmModelContainer db = new WebCrmModelContainer();
		public ActionResult Index()
		{
			var notes = db.NoteSet.ToList();
			var tasks = db.TaskSet.ToList();
			var dashboardViewModel = new DashboardViewModel
			{
				Tasks = tasks,
				Notes = notes
			};
			return View(dashboardViewModel);
		}
	}
}
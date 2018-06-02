using System.Web.Mvc;

namespace WebCrm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Company()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Person()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Task()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
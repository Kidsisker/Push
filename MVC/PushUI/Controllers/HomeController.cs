using System.Web.Mvc;

namespace PushUI.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Develop and utilize tools to automate tasks with a PUSH of a button!";

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "About Concord PUSH";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Contact the administrators of Concord PUSH";

			return View();
		}
	}
}

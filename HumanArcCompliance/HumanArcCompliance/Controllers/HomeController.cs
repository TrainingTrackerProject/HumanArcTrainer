using System.Web.Mvc;

namespace HumanArc.Compliance.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Blank");
            }
            return View("_Blank");
        }
    }
}

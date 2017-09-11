using System.Web.Mvc;

namespace HumanArc.Compliance.Web.Controllers
{
    [RoutePrefix("Assignment")]
    public class AssignmentController : Controller
    {
        [HttpGet]
        [Route("{trainingId?}")]
        public ActionResult Index(int? trainingId)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Blank");
            }
            return View("_Blank");
        }
    }
}
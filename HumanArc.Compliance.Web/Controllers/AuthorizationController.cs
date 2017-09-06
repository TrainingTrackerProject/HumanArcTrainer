
using System.Web.Mvc;
using HumanArc.Compliance.Shared.Interfaces;
using HumanArc.Compliance.Shared.ViewModels;
using HumanArc.Compliance.Web.Helpers;

namespace HumanArc.Compliance.Web.Controllers
{
    [RoutePrefix("Authorization")]
    public class AuthorizationController : Controller
    {
        private IAuthorizationService _authService;
        public AuthorizationController(IAuthorizationService authService)
        {
            _authService = authService;
        }

        // GET: Authorization
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Blank");
            }
            return View("_Blank");
        }

        [HttpGet]
        [Route("GetRolesAndUsers")]
        public ActionResult GetRolesAndUsers()
        {
            var roleData = _authService.GetRolesAndUsers();
            return JsonResponse.Success(roleData);
        }

        [HttpPost]
        [Route("Assign")]
        public ActionResult Assign(RoleAssignentViewModel contract)
        {
            var roleData = _authService.Assign(contract);
            return JsonResponse.Success(roleData);
        }

        [HttpPost]
        [Route("Remove")]
        public ActionResult Remove(RoleAssignentViewModel contract)
        {
            var roleData = _authService.Remove(contract);
            return JsonResponse.Success(roleData);
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (_authService.IsAuthorized(ActiveDirectoryHelper.GetCurrentUsersName(), SecuredOperationEnum.EditAuthorization))
        //    {
        //        base.OnActionExecuting(filterContext);
        //    }
        //    else
        //    {
        //        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        //    }
        //}
    }
}
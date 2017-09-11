using System.Web.Mvc;
using System.Web.Security;

namespace HumanArc.Compliance.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogIn()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Login([ModelBinder(typeof(DefaultModelBinder))] string userName, string password, string rememberMe, string returnUrl)
        {
            if (!ValidateLogOn(userName, password))
            {
                return View();
            }

            bool rememberMeVal = false;
            switch (rememberMe)
            {
                case "on":
                    rememberMeVal = true;
                    break;
            }
            FormsAuthentication.SetAuthCookie(userName, rememberMeVal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        private bool ValidateLogOn(string userName, string password)
        {
            //if (string.IsNullOrEmpty(userName))
            //{
            //    ModelState.AddModelError("usernameEx", "You must specify a username.");
            //}
            //if (string.IsNullOrEmpty(password))
            //{
            //    ModelState.AddModelError("passwordEx", "You must specify a password.");
            //}

            //if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            //{
            //    if (!Membership.Provider.ValidateUser(userName, password))
            //    {
            //        ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            //    }
            //}

            return ModelState.IsValid;
        }



    }
}

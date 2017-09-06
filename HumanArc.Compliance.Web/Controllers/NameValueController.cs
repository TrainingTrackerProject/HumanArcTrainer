using HumanArc.Compliance.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HumanArc.Compliance.Service.Implementation;
using HumanArc.Compliance.Shared.Helpers;

namespace HumanArc.Compliance.Web.Controllers
{
    [RoutePrefix("NameValue")]
    public class NameValueController : Controller
    {
        private AuthorizationService _authService;
        public NameValueController(AuthorizationService authService)
        {
            _authService = authService;
        }

        //[Route("GetGenericEnumList/{sourceSelectedValue}")]
        //[HttpGet]
        //public ActionResult GetGenericEnumList(string sourceSelectedValue)
        //{
        //    return new JsonNetResult { Data = ConvertDictToKeyValuePairList(ObjectHelper.GetEnumListFromName("HumanArc.Compliance.Shared", sourceSelectedValue))};
        //}

        private IEnumerable<KeyValuePair<TKey, TValue>> ConvertDictToKeyValuePairList<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            return dict.Select(t => new KeyValuePair<TKey, TValue>(t.Key, t.Value));
        }

        [Route("GetGroupList")]
        [HttpGet]
        public ActionResult GetGroupList ()
        {
            return new JsonNetResult { Data = ConvertDictToKeyValuePairList<string,string>(ActiveDirectoryHelper.GetADGroups())};
        }

        [Route("GetRoles")]
        public ActionResult GetRoles()
        {
            var userName = AuthHelper.GetUserName();
            if (userName != null)
            {
                var roles = _authService.GetRoles(userName);
                return new JsonNetResult {Data = roles.Select(x => new KeyValuePair<string, string>(x, x))};
            }
            return new JsonNetResult();
        }
    }
}
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Radin.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : RadinControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}
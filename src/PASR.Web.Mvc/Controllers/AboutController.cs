using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using PASR.Controllers;

namespace PASR.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : PASRControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}

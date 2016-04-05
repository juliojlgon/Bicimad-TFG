using System.Web.Mvc;

namespace Bicimad.Web.Areas.User.Controllers
{
    public partial class HomeController : UserBaseController
    {
        public HomeController()
        {

        }

        public virtual ActionResult Index()
        {
            return View(MVC.User.Home.Views.Index);
        }

    }

   
}
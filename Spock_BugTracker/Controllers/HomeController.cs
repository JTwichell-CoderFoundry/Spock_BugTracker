using Spock_BugTracker.Models;
using System.Web.Mvc;

namespace Spock_BugTracker.Controllers
{
    public class HomeController : Controller
    {
        //Use this as my main landing page and allow user to Login
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        //Otherwise redirect them to this custom Register view via a link...
        public ActionResult Register()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
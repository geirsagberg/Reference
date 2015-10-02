using System.Web.Mvc;
using Reference.BusinessLogic.Contracts;

namespace Reference.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonLogic personLogic;
        public HomeController(IPersonLogic personLogic)
        {
            this.personLogic = personLogic;
        }

        public ActionResult Index()
        {
            return View(personLogic.GetAll());
        }
    }
}
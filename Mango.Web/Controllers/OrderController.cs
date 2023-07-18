using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        public ActionResult OrderIndex()
        {
            return View();
        }

    }
}

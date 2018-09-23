using Microsoft.AspNetCore.Mvc;

namespace Notes.Web.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

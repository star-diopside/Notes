using Microsoft.AspNetCore.Mvc;
using Notes.Web.Services;

namespace Notes.Web.Controllers
{
    public class NotesController : Controller
    {
        private INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        public IActionResult Index()
        {
            return View(_noteService.SelectAll());
        }
    }
}

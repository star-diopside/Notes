using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Web.Models;
using Notes.Web.Services;
using System.Threading.Tasks;

namespace Notes.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UploadFilesController : Controller
    {
        private readonly IUploadFileService _uploadFileService;

        public UploadFilesController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uploadFileService.SelectAllAsync());
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UploadFileViewModel uploadFile)
        {
            try
            {
                await _uploadFileService.CreateAsync(uploadFile.ToUploadFile());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Notes.Web.Models;
using Notes.Web.Services;
using System.Threading.Tasks;

namespace Notes.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UploadFilesController : Controller
    {
        private readonly ILogger<UploadFilesController> _logger;
        private readonly IStringLocalizer<UploadFilesController> _localizer;
        private readonly IUploadFileService _uploadFileService;

        public UploadFilesController(ILogger<UploadFilesController> logger, IStringLocalizer<UploadFilesController> localizer,
            IUploadFileService uploadFileService)
        {
            _logger = logger;
            _localizer = localizer;
            _uploadFileService = uploadFileService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uploadFileService.ListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var uploadFile = await _uploadFileService.GetDetailsAsync(id);

            if (uploadFile == null)
            {
                return NotFound();
            }

            return View(uploadFile);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(RequiredUploadFileViewModel uploadFile)
        {
            if (ModelState.IsValid)
            {
                await _uploadFileService.CreateAsync(uploadFile);
                TempData["Success"] = _localizer["File was successfully uploaded."].Value;
                return RedirectToAction(nameof(Index));
            }
            return View(uploadFile);
        }

        public Task<IActionResult> Edit(int id) => Details(id);


        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Edit(int id, UploadFileViewModel uploadFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _uploadFileService.EditAsync(id, uploadFile);
                    TempData["Success"] = _localizer["File was successfully updated."].Value;
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    _logger.LogWarning(e, e.Message);
                    ViewData["Warning"] = _localizer["This data was updated by another user."];
                    return View(uploadFile);
                }
            }
            return View(uploadFile);
        }

        public Task<IActionResult> Delete(int id) => Details(id);

        [HttpPost]
        public async Task<IActionResult> Delete(int id, uint version)
        {
            try
            {
                await _uploadFileService.DeleteAsync(id, version);
                TempData["Success"] = _localizer["File was successfully deleted."].Value;
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogWarning(e, e.Message);
                ViewData["Warning"] = _localizer["This data was updated by another user."];
                return View(await _uploadFileService.GetDetailsAsync(id));
            }
        }

        public async Task<IActionResult> Download(int id)
        {
            var uploadFile = await _uploadFileService.GetDownloadDataAsync(id);

            if (uploadFile == null)
            {
                return NotFound();
            }

            return File(uploadFile.UploadFileData.Data, uploadFile.ContentType, uploadFile.FileName);
        }
    }
}

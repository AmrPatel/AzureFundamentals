using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobServices _blobServices;
        public BlobController(IBlobServices blobServices)
        {
            _blobServices = blobServices;
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string containerName)
        {
            var blobObj = await _blobServices.GetAllBlobs(containerName);
            return View(blobObj);
        }

        [HttpGet]
        public IActionResult AddFile(string containerName)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(string containerName, IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            var fileName = Path.GetFileNameWithoutExtension(file.Name) + "_" + Guid.NewGuid() + "_" + Path.GetExtension(file.Name);
            var result = _blobServices.UploadBlob(fileName, file, containerName);
            if (result != null)
                return RedirectToAction("Index", "Container");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(string name, string containerName)
        {
            return Redirect(await _blobServices.GetBlob(name, containerName));
        }

        public async Task<IActionResult> DeleteFile(string name, string containerName)
        {
            await _blobServices.DeleteBlob(name, containerName);
            return RedirectToAction("Index", "Home");
        }
    }
}

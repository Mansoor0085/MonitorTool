using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

public class FileController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

    public FileController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedFiles");
    }

    // Action to render the Files.cshtml page
    [HttpGet]
    public IActionResult Files()
    {
        // Define the directory where files are stored
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        // Get all uploaded file names
        var files = Directory.GetFiles(uploadFolder).Select(Path.GetFileName).ToList();
        ViewBag.Files = files;

        return View();
    }

    // Action to handle file uploads
    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string filePath = Path.Combine(uploadFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            TempData["Message"] = "Video uploaded successfully!";
        }
        else
        {
            TempData["Message"] = "File not found!";
        }
        return RedirectToAction("Files");
    }

    // Action to handle file deletions
    [HttpPost]
    public IActionResult Delete([FromForm] string fileName)
    {
        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedFiles", fileName);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            TempData["Message"] = "File deleted successfully!";
        }

        TempData["Message"] = "File not found.";
        return RedirectToAction("Files");
    }
}

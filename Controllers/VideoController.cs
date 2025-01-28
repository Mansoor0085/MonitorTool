using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;

namespace MonitorTool.Controllers
{
    public class VideoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _videoDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedVideos");


        // Inject IWebHostEnvironment to access application root paths
        public VideoController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _videoDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedVideos");
        }

        // GET: Video
        public IActionResult Index()
        {
            // Ensure the directory exists
            if (!Directory.Exists(_videoDirectory))
            {
                Directory.CreateDirectory(_videoDirectory);
            }

            // Get file names only (not full paths)
            var videos = Directory.GetFiles(_videoDirectory)
                                  .Select(video => Path.GetFileName(video)) // Make sure System.IO is imported
                                  .ToList()
                                  .OrderByDescending(x => x);

            ViewBag.Videos = videos;
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile videoFile)
        {
            if (videoFile != null && videoFile.Length > 0)
            {
                // Ensure the directory exists
                if (!Directory.Exists(_videoDirectory))
                {
                    Directory.CreateDirectory(_videoDirectory);
                }

                // Save the file to the directory
                var filePath = Path.Combine(_videoDirectory, videoFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    videoFile.CopyTo(stream);
                }

                TempData["Message"] = "Video uploaded successfully!";
            }
            else
            {
                TempData["Message"] = "Please select a video file.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string fileName)
        {
            var filePath = Path.Combine(_videoDirectory, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                TempData["Message"] = "Video deleted successfully!";
            }
            else
            {
                TempData["Message"] = "Video not found.";
            }

            return RedirectToAction("Index");
        }
        public ActionResult Recordings()
        {
            string videoDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedVideos");

            var videos = Directory.Exists(videoDirectory)
                ? Directory.GetFiles(videoDirectory)
                          .Select(Path.GetFileName)
                          .ToList()
                : new List<string>();

            ViewBag.Videos = videos;
            return View();
        }


    }
}
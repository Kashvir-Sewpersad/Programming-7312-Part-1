using Microsoft.AspNetCore.Mvc;
using Programming_7312_Part_1.Models;
using Programming_7312_Part_1.Services;
using System.IO;

namespace Programming_7312_Part_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IssueStorage _issueStorage;

        public HomeController(IssueStorage issueStorage)
        {
            _issueStorage = issueStorage ?? throw new ArgumentNullException(nameof(issueStorage));
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ReportIssues()
        {
            ViewBag.Categories = new[] { "Sanitation", "Roads", "Utilities", "Other" };
            return View(new Issue());
        }

        [HttpPost]
        public async Task<IActionResult> ReportIssues(Issue model, IFormFile attachment)
        {
            ViewBag.Categories = new[] { "Sanitation", "Roads", "Utilities", "Other" };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Handle file upload
            if (attachment != null && attachment.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Generate unique filename to avoid conflicts
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(attachment.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                // Save file asynchronously
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await attachment.CopyToAsync(stream);
                }

                model.AttachedFilePath = "/uploads/" + fileName;
            }

            // Store issue
            _issueStorage.ReportedIssues.AddLast(model);

            ViewBag.SuccessMessage = "Issue reported successfully!";
            ViewBag.EngagementMessage = "Your reports make our community better!";

            ModelState.Clear();
            return View(new Issue());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
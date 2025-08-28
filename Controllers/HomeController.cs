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
        public IActionResult ReportIssues(Issue model, IFormFile attachment)
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
                var filePath = Path.Combine(uploadsDir, attachment.FileName);
                var directory = Path.GetDirectoryName(filePath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    attachment.CopyTo(stream);
                }
                model.AttachedFilePath = "/uploads/" + attachment.FileName;
            }

            // Store issue
            _issueStorage.ReportedIssues.AddLast(model);

            ViewBag.SuccessMessage = "Issue reported successfully!";
            ViewBag.EngagementMessage = "Your reports make our community better!"; // Customize per Task 1

            ModelState.Clear();
            return View(new Issue());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
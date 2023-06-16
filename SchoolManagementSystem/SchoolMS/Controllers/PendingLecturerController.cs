using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolMS.Data;
using SchoolMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Controllers
{
    public class PendingLecturerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PendingLecturerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /PendingLecturer/Index
        public IActionResult Index()
        {
            ViewData["School"] = new SelectList(_dbContext.Schools, "Code", "Name");
            // Get error and success messages from TempData, if any
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View();
        }

        // POST: /PendingLecturer/Index
        [HttpPost]
        public IActionResult Index(PendingLecturer lecturer)
        {
            if (!ModelState.IsValid)
            {
                // If there are validation errors, store error message and redirect to the view
                TempData["ErrorMessage"] = "Please correct the errors.";
                return RedirectToAction("Index");
            }

            // Split the input string into a list of qualifications
            lecturer.Qualifications = SplitStringToList(lecturer.Qualifications);

            // Split the input string into a list of courses taught
            lecturer.CoursesTaught = SplitStringToList(lecturer.CoursesTaught);

            try
            {
                // Save the lecturer object to the database
                _dbContext.PendingLecturers.Add(lecturer);
                _dbContext.SaveChanges();

                // Store success message and redirect to the view
                TempData["SuccessMessage"] = "Lecturer added successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any database exception and store error message
                TempData["ErrorMessage"] = "An error occurred while saving the lecturer.";
                return RedirectToAction("Index");
            }
        }

        // Helper method to split a string into a list of strings
        private List<string> SplitStringToList(List<string> input)
        {
            return input?.SelectMany(item => item.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
}

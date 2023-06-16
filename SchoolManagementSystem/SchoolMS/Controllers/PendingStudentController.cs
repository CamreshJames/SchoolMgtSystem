using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolMS.Data;
using SchoolMS.Models;

namespace SchoolMS.Controllers
{
    public class PendingStudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PendingStudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Program"] = new SelectList(_context.Programs, "Code", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PendingStudent pendingstudent)
        {
            if (ModelState.IsValid)
            {
                _context.PendingStudents.Add(pendingstudent);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Successfully Applied! Please wait patiently for your application to be verified and your account to be created.";
                return RedirectToAction("Index", "Home");
            }

            // If there are model state errors, collect them and display as messages
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            ViewBag.ErrorMessages = errorMessages;

            // Return to the create form with the entered data and error messages
            return View(pendingstudent);
        }
    }
}
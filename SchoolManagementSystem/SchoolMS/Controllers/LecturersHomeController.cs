using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMS.Data;
using SchoolMS.Models;

namespace SchoolMS.Controllers
{
    public class LecturersHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturersHomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Lecturer") != null)
            {
                // Session exists, continue with the index page
                string lecturer = HttpContext.Session.GetString("Lecturer");

                var lec = _context.Lecturer.FirstOrDefault(a => a.UserName == lecturer);

                if (lec != null)
                {
                    ViewBag.lecturerName = lec.Name;
                    ViewBag.lecturerID = lec.UserName;
                    ViewBag.lecturerEmail = lec.Email;

                    return View();
                }
            }

            // Session doesn't exist or admin not found, redirect to the login page
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string lecturer, string password)
        {
            var lec = _context.Lecturer.FirstOrDefault(a => a.UserName == lecturer && a.Password == password);
            if (lec != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("Lecturer", lecturer);
                HttpContext.Session.SetString("Password", password);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "bad data";
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            string lecturer = HttpContext.Session.GetString("Lecturer");
            var lec = await _context.Lecturer
                .FirstOrDefaultAsync(m => m.UserName == lecturer);
            if (lec == null)
            {
                return NotFound();
            }

            return View(lec);
        }
        public async Task<IActionResult> UpdateProfile()
        {
            string lecturer = HttpContext.Session.GetString("Lecturer");
            var lec = await _context.Lecturer.FirstOrDefaultAsync(m => m.UserName == lecturer);
            return View(lec);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(int id, [Bind("Id,StudyProgram,Name,Email,PhoneNumber,Gender,DateOfBirth,GuardianName,GuardianPhone,Nationality,NationalId,County,Address")] Lecturer lecturer)
        {
            if (id != lecturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var ex = new Exception();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Lecturer");
            return RedirectToAction("Index", "Home");
        }
    }
}

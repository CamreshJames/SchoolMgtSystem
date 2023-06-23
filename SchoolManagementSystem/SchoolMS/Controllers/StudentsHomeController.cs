using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolMS.Data;
using SchoolMS.Models;

namespace SchoolMS.Controllers
{
    public class StudentsHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsHomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Student") != null)
            {
                // Session exists, continue with the index page
                string stdid = HttpContext.Session.GetString("Student");

                var student = _context.Students.FirstOrDefault(a => a.StudentId == stdid);

                if (student != null)
                {
                    var noticeBoard = _context.NoticeBoard.ToList();
                    var noticeCount = noticeBoard.Count();
                    
                    ViewBag.NoticeCount = noticeCount;
                    ViewBag.stdName =student.Name;
                    ViewBag.stdID =student.StudentId;
                    ViewBag.stdEmail = student.Email;
                    string studentId = HttpContext.Session.GetString("Student");
                    if (student == null)
                    {
                        // Handle the case where the student is not found
                        // You can redirect to an error page or return an appropriate response
                        return NotFound();
                    }

                    var program = student.StudyProgram; // Assuming the program is stored in the StudyProgram property of the student
                    var programs = _context.Programs.FirstOrDefault(s => s.Code == program);
                    var programname = programs.Name;
                    var courses = _context.Courses.Where(c => c.ProgramName == programname).ToList();

                    // Create a view model to hold the student information and courses
                    var viewModel = new CourseViewModel
                    {
                        StudentName = student.Name,
                        Program = program,
                        Courses = courses
                    };

                    return View(viewModel);
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
        public IActionResult Login(string stdid, string password)
        {
            var student = _context.Students.FirstOrDefault(a => a.StudentId == stdid && a.Password == password);
            if (student != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("Student", stdid);
                HttpContext.Session.SetString("Password", password);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "bad data";
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            string stdid = HttpContext.Session.GetString("Student");
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == stdid);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public async Task<IActionResult> UpdateProfile()
        {
            string stdid = HttpContext.Session.GetString("Student");
            var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == stdid);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(int id, [Bind("Id,StudyProgram,Name,Email,PhoneNumber,Gender,DateOfBirth,GuardianName,GuardianPhone,Nationality,NationalId,County,Address")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var ex = new Exception();
                }
                return RedirectToAction(nameof(Profile));
            }
            return View(student);
        }
      
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Student");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Status()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Status(string Name, string Nationalid)
        {
            var student = _context.Students.FirstOrDefault(a => a.Name == Name && a.Password == Nationalid);
            if (student == null)
            {
                string message = "Your application is yet to be reviewed";
                return RedirectToAction("StatusDetails", new { message, isStudentValid = false });
            }
            else
            {
                string regNumber = student.StudentId;
                string message = $"Click the Student Portal and enter your registration number, which is {regNumber}, and the default password is your National ID";
                return RedirectToAction("StatusDetails", new { message, isStudentValid = true });
            }
        }

        public IActionResult StatusDetails(string message, bool isStudentValid)
        {
            ViewBag.Message = message;
            ViewBag.IsStudentValid = isStudentValid;
            return View();
        }
        public IActionResult NoticeBoard()
        {
            var noticeBoard = _context.NoticeBoard.ToList();

            return View(noticeBoard);
        }

        public IActionResult Courses()
        {
            string studentId = HttpContext.Session.GetString("Student");
            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                // Handle the case where the student is not found
                // You can redirect to an error page or return an appropriate response
                return NotFound();
            }

            var program = student.StudyProgram; // Assuming the program is stored in the StudyProgram property of the student
            var programs = _context.Programs.FirstOrDefault(s => s.Code == program);
            var programname = programs.Name;
            var courses = _context.Courses.Where(c => c.ProgramName == programname).ToList();

            // Create a view model to hold the student information and courses
            var viewModel = new CourseViewModel
            {
                StudentName = student.Name,
                Program = program,
                Courses = courses
            };

            return View(viewModel);
        }
        public IActionResult ClubRegister()
        {
            string studentId = HttpContext.Session.GetString("Student");
            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);
            var studentname = student.Name;
            ViewBag.StudentName = studentname;
            ViewData["Clubs"] = new SelectList(_context.Clubs,"ClubId","Name");
            List<Club> clubs = _context.Clubs.ToList(); // Assuming you have a "Clubs" DbSet in your DbContext
            ViewBag.ClubsList = clubs;

            return View();
        }

        // POST: ClubMember/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClubRegister([Bind("ClubMemberId,StudentName,UserName,Password,ClubName")] ClubMember clubMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clubMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ClubRegister));
            }
            return View(clubMember);
        }

    }
}
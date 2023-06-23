using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolMS.Data;
using SchoolMS.Models;

namespace SchoolMS.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudyProgram,Name,Email,PhoneNumber,Gender,GuardianName,GuardianPhone,Nationality,NationalId,County,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

       

        public async Task<IActionResult> PendingStudents()
        {
            return _context.PendingStudents != null ?
                        View(await _context.PendingStudents.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PendingStudents'  is null.");
        }
        public async Task<IActionResult> PendingStudentDetails(int? id)
        {
            if (id == null || _context.PendingStudents == null)
            {
                return NotFound();
            }

            var student = await _context.PendingStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        // GET: Students/Delete/5
        public async Task<IActionResult> Reject(int? id)
        {
            if (id == null || _context.PendingStudents == null)
            {
                return NotFound();
            }

            var student = await _context.PendingStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectionsConfirmed(int id)
        {
            if (_context.PendingStudents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PendingStudents'  is null.");
            }
            var student = await _context.PendingStudents.FindAsync(id);
            if (student != null)
            {
                _context.PendingStudents.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PendingStudents));
        }
        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null || _context.PendingStudents == null)
            {
                return NotFound();
            }

            var student= await _context.PendingStudents.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        public string GenerateRegistrationNumber()
        {
            // Define the range of characters and numbers to use for the registration number
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";

            // Generate a random registration number
            Random random = new Random();
            string registrationNumber = "";

            // Add two random letters from the character set
            registrationNumber += characters[random.Next(characters.Length)];
            registrationNumber += characters[random.Next(characters.Length)];

            // Add four random numbers from the number set
            for (int i = 0; i < 4; i++)
            {
                registrationNumber += numbers[random.Next(numbers.Length)];
            }

            // Return the generated registration number
            return registrationNumber;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id, [Bind("Id,StudyProgram,Name,Email,PhoneNumber,Gender,GuardianName,GuardianPhone,Nationality,NationalId,County,Address")] PendingStudent student)
        {
            if (ModelState.IsValid)
            {
                var pendingStudent = await _context.PendingStudents.FindAsync(id);

                if (pendingStudent == null)
                {
                    return NotFound();
                }

                var program = pendingStudent.StudyProgram;
                var pcode = await _context.Programs
                    .Where(p => p.Name == program)
                    .Select(p => p.Code)
                    .FirstOrDefaultAsync();

                // Generate a random registration number
                string registrationNumber = GenerateRegistrationNumber();

                // Combine pcode, registrationNumber, and current year as StudentId
                int currentYear = DateTime.Now.Year;
                string Studentid = $"{program}/{registrationNumber}/{currentYear}";

                // Use the StudentId as needed

                var newStudent = new Student
                {
                    // Assign properties from the pending student to the new student
                    Id = pendingStudent.Id,
                    StudyProgram = pendingStudent.StudyProgram,
                    Name = pendingStudent.Name,
                    Email = pendingStudent.Email,
                    PhoneNumber = pendingStudent.PhoneNumber,
                    Gender = pendingStudent.Gender,
                    GuardianName = pendingStudent.GuardianName,
                    GuardianPhone = pendingStudent.GuardianPhone,
                    Nationality = pendingStudent.Nationality,
                    NationalId = pendingStudent.NationalId,
                    County = pendingStudent.County,
                    Address = pendingStudent.Address,
                    ProfileImage = "~/ProfileImages/Profileplaceholder.png",
                    StudentId = Studentid,
                    Password  = pendingStudent.NationalId,
                };

                _context.Students.Add(newStudent);
                _context.PendingStudents.Remove(pendingStudent);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(PendingStudents));
            }

            return View(student);
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, StudyProgram, StudentId, Name, Email, PhoneNumber, Gender, GuardianName, GuardianPhone, Nationality, NationalId, County, Address, ProfileImage, Password")] Student student)
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
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }


        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

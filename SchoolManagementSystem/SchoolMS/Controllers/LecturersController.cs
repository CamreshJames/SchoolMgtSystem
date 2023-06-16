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
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lecturers
        public async Task<IActionResult> Index()
        {
              return _context.Lecturer != null ? 
                          View(await _context.Lecturer.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Lecturer'  is null.");
        }

        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lecturer == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,School,Name,UserName,Email,PhoneNumber,Gender,DateOfBirth,Address,Qualifications,Position,Specialization,CoursesTaught,OfficeLocation,OfficeHours,Nationality,NationalId")] Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }
        public async Task<IActionResult> PendingLecturers()
        {
            return _context.PendingLecturers != null ?
                        View(await _context.PendingLecturers.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PendingLecturers'  is null.");
        }
        public async Task<IActionResult> PendingLecturerDetails(int? id)
        {
            if (id == null || _context.PendingLecturers == null)
            {
                return NotFound();
            }

            var lecturer = await _context.PendingLecturers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }
        // GET: Students/Delete/5
        public async Task<IActionResult> Reject(int? id)
        {
            if (id == null || _context.PendingLecturers == null)
            {
                return NotFound();
            }

            var lecturer= await _context.PendingLecturers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
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
            var lecturer = await _context.PendingStudents.FindAsync(id);
            if (lecturer != null)
            {
                _context.PendingStudents.Remove(lecturer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PendingLecturers));
        }
        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null || _context.PendingLecturers == null)
            {
                return NotFound();
            }

            var lecturer = await _context.PendingLecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            return View(lecturer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id, [Bind("Id,School,Name,UserName,Email,PhoneNumber,Gender,Address,Qualifications,Position,Specialization,CoursesTaught,OfficeLocation,OfficeHours,Nationality,NationalId")] PendingLecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                var pendingLecturer = await _context.PendingLecturers.FindAsync(id);

                if (pendingLecturer == null)
                {
                    return NotFound();
                }

                var newLecturer = new Lecturer
                {
                    Id = pendingLecturer.Id,
                    School = pendingLecturer.School,
                    Name = pendingLecturer.Name,
                    UserName = pendingLecturer.UserName,
                    Email = pendingLecturer.Email,
                    PhoneNumber = pendingLecturer.PhoneNumber,
                    Gender = pendingLecturer.Gender,
                    Address = pendingLecturer.Address,
                    Qualifications = pendingLecturer.Qualifications,
                    Position = pendingLecturer.Position,
                    Specialization = pendingLecturer.Specialization,
                    CoursesTaught = pendingLecturer.CoursesTaught,
                    OfficeLocation = pendingLecturer.OfficeLocation,
                    OfficeHours = pendingLecturer.OfficeHours,
                    Nationality = pendingLecturer.Nationality,
                    NationalId = pendingLecturer.NationalId
                };

                _context.Lecturer.Add(newLecturer);
                _context.PendingLecturers.Remove(pendingLecturer);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(PendingLecturers));
            }

            return View(lecturer);
        }


        // GET: Lecturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lecturer == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            return View(lecturer);
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,School,Name,UserName,Email,PhoneNumber,Gender,DateOfBirth,Address,Qualifications,Position,Specialization,CoursesTaught,OfficeLocation,OfficeHours,Nationality,NationalId")] Lecturer lecturer)
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
                    if (!LecturerExists(lecturer.Id))
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
            return View(lecturer);
        }

        // GET: Lecturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lecturer == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lecturer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lecturer'  is null.");
            }
            var lecturer = await _context.Lecturer.FindAsync(id);
            if (lecturer != null)
            {
                _context.Lecturer.Remove(lecturer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerExists(int id)
        {
          return (_context.Lecturer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

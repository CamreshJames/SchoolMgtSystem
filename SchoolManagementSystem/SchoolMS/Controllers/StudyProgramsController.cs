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
    public class StudyProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudyProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudyPrograms
        public async Task<IActionResult> Index()
        {
            return _context.Students != null ?
                         View(await _context.Programs.ToListAsync()) :
                         Problem("Entity set 'ApplicationDbContext.Programs'  is null.");
        }

        // GET: StudyPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Programs == null)
            {
                return NotFound();
            }

            var studyProgram = await _context.Programs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyProgram == null)
            {
                return NotFound();
            }

            return View(studyProgram);
        }

        // GET: StudyPrograms/Create
        public IActionResult Create()
        {
            ViewData["School"] = new SelectList(_context.Schools, "Code", "Name");
            return View();
        }

        // POST: StudyPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Description,Coordinator,Duration,Requirements,Prerequisite,DegreeType,School")] StudyProgram studyProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["School"] = new SelectList(_context.Schools, "Code", "Name", studyProgram.School);
            return View(studyProgram);
        }

        // GET: StudyPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Programs == null)
            {
                return NotFound();
            }

            var studyProgram = await _context.Programs.FindAsync(id);
            if (studyProgram == null)
            {
                return NotFound();
            }
            ViewData["School"] = new SelectList(_context.Schools, "Id", "Name", studyProgram.School);
            return View(studyProgram);
        }

        // POST: StudyPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,Description,Coordinator,Duration,Requirements,Prerequisite,DegreeType,SchoolId")] StudyProgram studyProgram)
        {
            if (id != studyProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyProgramExists(studyProgram.Id))
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
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "ChairPerson", studyProgram.School);
            return View(studyProgram);
        }

        // GET: StudyPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Programs == null)
            {
                return NotFound();
            }

            var studyProgram = await _context.Programs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyProgram == null)
            {
                return NotFound();
            }

            return View(studyProgram);
        }

        // POST: StudyPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Programs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Programs'  is null.");
            }
            var studyProgram = await _context.Programs.FindAsync(id);
            if (studyProgram != null)
            {
                _context.Programs.Remove(studyProgram);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyProgramExists(int id)
        {
          return (_context.Programs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

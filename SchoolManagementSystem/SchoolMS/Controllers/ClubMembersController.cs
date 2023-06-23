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
    public class ClubMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClubMembers
        public async Task<IActionResult> Index()
        {
              return _context.ClubsMembers != null ? 
                          View(await _context.ClubsMembers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ClubsMembers'  is null.");
        }

        // GET: ClubMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClubsMembers == null)
            {
                return NotFound();
            }

            var clubMember = await _context.ClubsMembers
                .FirstOrDefaultAsync(m => m.ClubMemberId == id);
            if (clubMember == null)
            {
                return NotFound();
            }

            return View(clubMember);
        }

        // GET: ClubMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClubMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClubMemberId,StudentName,UserName,Password,ClubName")] ClubMember clubMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clubMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clubMember);
        }

        // GET: ClubMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClubsMembers == null)
            {
                return NotFound();
            }

            var clubMember = await _context.ClubsMembers.FindAsync(id);
            if (clubMember == null)
            {
                return NotFound();
            }
            return View(clubMember);
        }

        // POST: ClubMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClubMemberId,StudentName,UserName,Password,ClubName")] ClubMember clubMember)
        {
            if (id != clubMember.ClubMemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubMemberExists(clubMember.ClubMemberId))
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
            return View(clubMember);
        }

        // GET: ClubMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClubsMembers == null)
            {
                return NotFound();
            }

            var clubMember = await _context.ClubsMembers
                .FirstOrDefaultAsync(m => m.ClubMemberId == id);
            if (clubMember == null)
            {
                return NotFound();
            }

            return View(clubMember);
        }

        // POST: ClubMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClubsMembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClubsMembers'  is null.");
            }
            var clubMember = await _context.ClubsMembers.FindAsync(id);
            if (clubMember != null)
            {
                _context.ClubsMembers.Remove(clubMember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubMemberExists(int id)
        {
          return (_context.ClubsMembers?.Any(e => e.ClubMemberId == id)).GetValueOrDefault();
        }
    }
}

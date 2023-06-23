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
    public class NoticeBoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticeBoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NoticeBoard
        public async Task<IActionResult> Index()
        {
              return _context.NoticeBoard != null ? 
                          View(await _context.NoticeBoard.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NoticeBoard'  is null.");
        }

        // GET: NoticeBoard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NoticeBoard == null)
            {
                return NotFound();
            }

            var noticeBoard = await _context.NoticeBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeBoard == null)
            {
                return NotFound();
            }

            return View(noticeBoard);
        }

        // GET: NoticeBoard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NoticeBoard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Description,Receiver, Sender")] NoticeBoard noticeBoard)
        {
            if (ModelState.IsValid)
            {
                noticeBoard.Sender = "Iday";

                _context.Add(noticeBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(noticeBoard);
        }

        // GET: NoticeBoard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NoticeBoard == null)
            {
                return NotFound();
            }

            var noticeBoard = await _context.NoticeBoard.FindAsync(id);
            if (noticeBoard == null)
            {
                return NotFound();
            }
            return View(noticeBoard);
        }

        // POST: NoticeBoard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Description,Receiver, Sender")] NoticeBoard noticeBoard)
        {
            if (id != noticeBoard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticeBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticeBoardExists(noticeBoard.Id))
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
            return View(noticeBoard);
        }

        // GET: NoticeBoard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NoticeBoard == null)
            {
                return NotFound();
            }

            var noticeBoard = await _context.NoticeBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeBoard == null)
            {
                return NotFound();
            }

            return View(noticeBoard);
        }

        // POST: NoticeBoard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NoticeBoard == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NoticeBoard'  is null.");
            }
            var noticeBoard = await _context.NoticeBoard.FindAsync(id);
            if (noticeBoard != null)
            {
                _context.NoticeBoard.Remove(noticeBoard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticeBoardExists(int id)
        {
          return (_context.NoticeBoard?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFood.Data;
using FastFood.Models;

namespace FastFood.Controllers
{
    public class PlatesController : Controller
    {
        private readonly FastFoodContext _context;

        public PlatesController(FastFoodContext context)
        {
            _context = context;
        }
        // GET: Plates
        public async Task<IActionResult> Index()
        {

            return View(await _context.Plate.ToListAsync());
        }

        // GET: Plates
        public async Task<IActionResult> List()
        {

            return View(await _context.Plate.ToListAsync());
        }

        // GET: Plates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plate == null)
            {
                return NotFound();
            }

            var plate = await _context.Plate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plate == null)
            {
                return NotFound();
            }

            return View(plate);
        }
        public async Task<IActionResult> Eye(int? id)
        {
            if (id == null || _context.Plate == null)
            {
                return NotFound();
            }

            var plate = await _context.Plate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plate == null)
            {
                return NotFound();
            }

            return View(plate);
        }
        // GET: Plates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Photo")] Plate plate)
        {
            if (ModelState.IsValid)
            {
                plate.Photo="../lib/Image/"+plate.Photo ;
                _context.Add(plate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plate);
        }

        // GET: Plates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plate == null)
            {
                return NotFound();
            }

            var plate = await _context.Plate.FindAsync(id);
            if (plate == null)
            {
                return NotFound();
            }
            return View(plate);
        }

        // POST: Plates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Photo")] Plate plate)
        {
            if (id != plate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    plate.Photo = "../lib/Image/" + plate.Photo;
                    _context.Update(plate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlateExists(plate.Id))
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
            return View(plate);
        }

        // GET: Plates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Plate == null)
            {
                return NotFound();
            }

            var plate = await _context.Plate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plate == null)
            {
                return NotFound();
            }

            return View(plate);
        }

        // POST: Plates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Plate == null)
            {
                return Problem("Entity set 'FastFoodContext.Plate'  is null.");
            }
            var plate = await _context.Plate.FindAsync(id);
            if (plate != null)
            {
                _context.Plate.Remove(plate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlateExists(int id)
        {
          return _context.Plate.Any(e => e.Id == id);
        }
    }
}

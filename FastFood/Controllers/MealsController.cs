using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFood.Data;
using FastFood.Models;
using Microsoft.AspNetCore.Authorization;

namespace FastFood.Controllers
{
    public class MealsController : Controller
    {
        private readonly FastFoodContext _context;

        public MealsController(FastFoodContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]

        // GET: Meals
        public async Task<IActionResult> Index()
        {
            //var hottellerieDbContext = _context.Meal.Include(a => a.Plate);

            /*IQueryable<Meal> query =
                 from meal in _context.Meal
                 join plate in _context.Plate on meal.Plate equals plate
                 select meal;
            */
            //return View(query.ToList());
            return View(await _context.Meal.Include(a => a.Plate).ToListAsync());
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }
        [Authorize(Roles = "Admin")]

        // GET: Meals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }
        [Authorize(Roles = "Admin")]

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
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
            return View(meal);
        }

        public async Task<IActionResult> Plus(int id)
        {
            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            meal.Number = meal.Number + 1 ;
            _context.Update(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction("LastOrder", "Orders");
        }
        public async Task<IActionResult> moin(int id)
        {
            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            meal.Number = meal.Number - 1;
            _context.Update(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction("LastOrder", "Orders");
        }
        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meal == null)
            {
                return Problem("Entity set 'FastFoodContext.Meal'  is null.");
            }
            var meal = await _context.Meal.FindAsync(id);
            if (meal != null)
            {
                _context.Meal.Remove(meal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("LastOrder", "Orders");
        }

        private bool MealExists(int id)
        {
          return _context.Meal.Any(e => e.Id == id);
        }
    }
}

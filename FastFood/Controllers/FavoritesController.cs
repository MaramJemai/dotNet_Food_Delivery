using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFood.Data;
using FastFood.Models;
using Microsoft.AspNetCore.Identity;
using FastFood.Areas.Identity.Data;

namespace FastFood.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly FastFoodContext _context;
        private readonly UserManager<FastFoodUser> _userManager;

        public FavoritesController(FastFoodContext context, UserManager<FastFoodUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        private Task<FastFoodUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Favorites
        public async Task<IActionResult> Index()
        {
              return View(await _context.Favorite
                .Include(a => a.Plate)
               // .Where(async a => a.Client == await GetCurrentUserAsync())
                 .ToListAsync());
        }

        // GET: Favorites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Favorite == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // GET: Favorites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Favorites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favorite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favorite);
        }

        public async Task<IActionResult> Add(int id)
        {
            Favorite favorite = new Favorite();
            favorite.Plate = await _context.Plate.FindAsync(id);
            favorite.Client = await _userManager.GetUserAsync(HttpContext.User);
            _context.Add(favorite);
            await _context.SaveChangesAsync();
            return RedirectToAction("List","Plates");
           
        }

        // GET: Favorites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Favorite == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Favorite favorite)
        {
            if (id != favorite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favorite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteExists(favorite.Id))
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
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Favorite == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Favorite == null)
            {
                return Problem("Entity set 'FastFoodContext.Favorite'  is null.");
            }
            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite != null)
            {
                _context.Favorite.Remove(favorite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteExists(int id)
        {
          return _context.Favorite.Any(e => e.Id == id);
        }
    }
}

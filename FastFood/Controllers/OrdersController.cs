using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFood.Data;
using FastFood.Models;
using FastFood.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using System.Collections.Immutable;

namespace FastFood.Controllers
{
    public class OrdersController : Controller
    {
        private readonly FastFoodContext _context;
        private readonly FastFoodContext _context0;
        private readonly FastFoodContext _context1;
        private readonly FastFoodContext _context2;
        private readonly FastFoodContext _context3;
        private readonly FastFoodContext _context4;
        private readonly FastFoodContext _context5;
        private readonly FastFoodContext _context6;
        private readonly FastFoodContext _context7;
        private readonly UserManager<FastFoodUser> _userManager;
        public OrdersController(FastFoodContext context , UserManager<FastFoodUser> userManager)
        {
            _context = context;
            _context0 = context;
            _context1 = context;
            _context2 = context;
            _context3 = context;
            _context4 = context;
            _context5 = context;
            _context6 = context;
            _context7 = context;
            _userManager = userManager;
        }
        private Task<FastFoodUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Orders
        public async Task<IActionResult> Index()
        {
              //await AddToOrder(1);
              return View(await _context.Order.ToListAsync());
        }
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(a => a.Meals)
                .ThenInclude(a => a.Plate)
                .FirstOrDefaultAsync(m => m.Id == id);

      if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // GET: Orders/Details/5
        public async Task<IActionResult> LastOrder()
        {
            FastFoodUser usr = await GetCurrentUserAsync();
            var order = await _context.Order
                .Include(a => a.Meals)
                .ThenInclude(a => a.Plate)
                .FirstOrDefaultAsync(i => i.State == EnumOrderState.Waiting && i.Client == usr);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        public async Task<IActionResult> ConfirmOrder(int id )
        {
            Order order;
            order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            order.State = EnumOrderState.Completed;
            _context1.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Total,PayementMode,State")] Order order)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        public async Task<IActionResult> AddToOrder(int id)
        {
            FastFoodUser usr = await GetCurrentUserAsync();
            Order? order;
            Plate? plate = await _context3.Plate.FindAsync(id);
            Order? resultat = _context4.Order.OrderBy(e => e.Id).LastOrDefault<Order>();
            if(resultat?.State== EnumOrderState.Completed)
            {
                order = new Order();
                order.Date = DateTime.Now;
                order.Total = 0;
                order.State = EnumOrderState.Waiting;
                order.PayementMode =   "credit card" ;
                order.Client = usr;
                 _context2.Add(order);
                await _context7.SaveChangesAsync();
                Meal meal = new Meal();
                meal.Number = 1;
                meal.Order = order;
                meal.Plate = plate;
                _context2.Add(meal);
                await _context7.SaveChangesAsync();
                return RedirectToAction("List", "Plates");
            }
            else
            {
                order = resultat;
                Meal meal;
                Meal? resultat1 = _context1.Meal.OrderBy(e => e.Id).LastOrDefault(m => m.Order == order && m.Plate == plate);

                // await AddToMeal(order, plate);
                if (resultat1 != null)
                {
                    meal = resultat1;
                    meal.Number = meal.Number + 1;
                    _context0.Update(meal);
                    await _context0.SaveChangesAsync();
                }
                else
                {
                    meal = new Meal();
                    meal.Number = 1;
                    meal.Order = order;
                    meal.Plate = plate;
                    _context2.Add(meal);
                    await _context7.SaveChangesAsync();
                }
                return RedirectToAction("List", "Plates");
            }
        }
        public async Task AddToMeal(Order? order, Plate? plate)
        {
            Meal meal ;
            Meal? resultat =  _context1.Meal.OrderBy(e => e.Id).LastOrDefault(m => m.Order == order && m.Plate== plate);
            /*var requette = from m in _context.Meal
                           orderby m.Id ascending
                           where m.Plate == plate 
                           where m.Order == order
                           select m;
            Meal resultat= requette.Last<Meal>();*/
            if (resultat != null)
            {
                meal = resultat;
                meal.Number = meal.Number ++ ;
                _context0.Update(meal);
                await _context0.SaveChangesAsync();
            }
            else
            {
                meal = new Meal();
                meal.Number = 1;
                meal.Order = order;
                meal.Plate= plate;
                _context2.Add(meal);
                await _context7.SaveChangesAsync();
            }
        }
      
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Total,PayementMode,State")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }
            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'FastFoodContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool OrderExists(int id)
        {
          return _context.Order.Any(e => e.Id == id);
        }
    }
}
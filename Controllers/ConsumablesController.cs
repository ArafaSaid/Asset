using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asset.Data;
using Asset.Models;

namespace Asset.Controllers
{
    public class ConsumablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsumablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consumables
        public async Task<IActionResult> Index()
        {
            var consumables = await _context.Consumables
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return View(consumables);
        }

        // GET: Consumables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumables
                .FirstOrDefaultAsync(m => m.ConsumableId == id);
            
            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }

        // GET: Consumables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consumables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsumableId,Name,Category,PartNumber,Manufacturer,Supplier,UnitPrice,QuantityInStock,MinimumStockLevel,ReorderQuantity,UnitOfMeasure,Location,Description,LastOrderedDate,LastReceivedDate,IsActive,Notes,CreatedBy")] Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                consumable.CreatedAt = DateTime.UtcNow;
                _context.Add(consumable);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Consumable created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(consumable);
        }

        // GET: Consumables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumables.FindAsync(id);
            if (consumable == null)
            {
                return NotFound();
            }
            return View(consumable);
        }

        // POST: Consumables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsumableId,Name,Category,PartNumber,Manufacturer,Supplier,UnitPrice,QuantityInStock,MinimumStockLevel,ReorderQuantity,UnitOfMeasure,Location,Description,LastOrderedDate,LastReceivedDate,IsActive,Notes,CreatedAt,CreatedBy,UpdatedBy")] Consumable consumable)
        {
            if (id != consumable.ConsumableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    consumable.UpdatedAt = DateTime.UtcNow;
                    _context.Update(consumable);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Consumable updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumableExists(consumable.ConsumableId))
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
            return View(consumable);
        }

        // GET: Consumables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumables
                .FirstOrDefaultAsync(m => m.ConsumableId == id);
            
            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }

        // POST: Consumables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumable = await _context.Consumables.FindAsync(id);
            if (consumable != null)
            {
                _context.Consumables.Remove(consumable);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Consumable deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Consumables/LowStock
        public async Task<IActionResult> LowStock()
        {
            var lowStockItems = await _context.Consumables
                .Where(c => c.QuantityInStock <= c.MinimumStockLevel && c.IsActive)
                .OrderBy(c => c.QuantityInStock)
                .ToListAsync();
            
            return View(lowStockItems);
        }

        private bool ConsumableExists(int id)
        {
            return _context.Consumables.Any(e => e.ConsumableId == id);
        }
    }
}

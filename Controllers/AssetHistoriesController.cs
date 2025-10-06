using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Data;
using Asset.Models;

namespace Asset.Controllers
{
    public class AssetHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssetHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssetHistories.Include(h => h.ActionType).Include(h => h.Asset);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssetHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssetHistory = await _context.AssetHistories
                .Include(h => h.ActionType)
                .Include(h => h.Asset)
                .FirstOrDefaultAsync(m => m.HistoryID == id);
            if (hdAssetHistory == null)
            {
                return NotFound();
            }

            return View(hdAssetHistory);
        }

        // GET: AssetHistories/Create
        public IActionResult Create()
        {
            ViewData["ActionTypeId"] = new SelectList(_context.AssetActionTypes, "ActionTypeId", "Name");
            ViewData["AssetID"] = new SelectList(_context.Assets, "AssetID", "AssetID");
            return View();
        }

        // POST: AssetHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryID,AssetID,ActionTypeId,Description,SparePart,PerformedBy,AssignedToUser,ActionDate,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] hdAssetHistory hdAssetHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hdAssetHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionTypeId"] = new SelectList(_context.AssetActionTypes, "ActionTypeId", "Name", hdAssetHistory.ActionTypeId);
            ViewData["AssetID"] = new SelectList(_context.Assets, "AssetID", "AssetID", hdAssetHistory.AssetID);
            return View(hdAssetHistory);
        }

        // GET: AssetHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssetHistory = await _context.AssetHistories.FindAsync(id);
            if (hdAssetHistory == null)
            {
                return NotFound();
            }
            ViewData["ActionTypeId"] = new SelectList(_context.AssetActionTypes, "ActionTypeId", "Name", hdAssetHistory.ActionTypeId);
            ViewData["AssetID"] = new SelectList(_context.Assets, "AssetID", "AssetID", hdAssetHistory.AssetID);
            return View(hdAssetHistory);
        }

        // POST: AssetHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryID,AssetID,ActionTypeId,Description,SparePart,PerformedBy,AssignedToUser,ActionDate,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] hdAssetHistory hdAssetHistory)
        {
            if (id != hdAssetHistory.HistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hdAssetHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hdAssetHistoryExists(hdAssetHistory.HistoryID))
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
            ViewData["ActionTypeId"] = new SelectList(_context.AssetActionTypes, "ActionTypeId", "Name", hdAssetHistory.ActionTypeId);
            ViewData["AssetID"] = new SelectList(_context.Assets, "AssetID", "AssetID", hdAssetHistory.AssetID);
            return View(hdAssetHistory);
        }

        // GET: AssetHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssetHistory = await _context.AssetHistories
                .Include(h => h.ActionType)
                .Include(h => h.Asset)
                .FirstOrDefaultAsync(m => m.HistoryID == id);
            if (hdAssetHistory == null)
            {
                return NotFound();
            }

            return View(hdAssetHistory);
        }

        // POST: AssetHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hdAssetHistory = await _context.AssetHistories.FindAsync(id);
            if (hdAssetHistory != null)
            {
                _context.AssetHistories.Remove(hdAssetHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hdAssetHistoryExists(int id)
        {
            return _context.AssetHistories.Any(e => e.HistoryID == id);
        }
    }
}

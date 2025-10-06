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
    public class AssetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Assets.Include(h => h.hdAssetTypes).Include(h => h.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssets = await _context.Assets
                .Include(h => h.hdAssetTypes)
                .Include(h => h.Status)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (hdAssets == null)
            {
                return NotFound();
            }

            return View(hdAssets);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            ViewData["TypeID"] = new SelectList(_context.AssetTypes, "TypeID", "Name");
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetID,ComputerName,UserName,TypeID,SerialNumber,Model,Manufacturer,Supplier,Processor,RAM,Storage,OS,Monitor,Printer,PurchaseDate,PurchasePrice,WarrantyExpirationDate,Location,StatusId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] hdAssets hdAssets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hdAssets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeID"] = new SelectList(_context.AssetTypes, "TypeID", "Name", hdAssets.TypeID);
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssets.StatusId);
            return View(hdAssets);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssets = await _context.Assets.FindAsync(id);
            if (hdAssets == null)
            {
                return NotFound();
            }
            ViewData["TypeID"] = new SelectList(_context.AssetTypes, "TypeID", "Name", hdAssets.TypeID);
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssets.StatusId);
            return View(hdAssets);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetID,ComputerName,UserName,TypeID,SerialNumber,Model,Manufacturer,Supplier,Processor,RAM,Storage,OS,Monitor,Printer,PurchaseDate,PurchasePrice,WarrantyExpirationDate,Location,StatusId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] hdAssets hdAssets)
        {
            if (id != hdAssets.AssetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hdAssets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hdAssetsExists(hdAssets.AssetID))
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
            ViewData["TypeID"] = new SelectList(_context.AssetTypes, "TypeID", "Name", hdAssets.TypeID);
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssets.StatusId);
            return View(hdAssets);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssets = await _context.Assets
                .Include(h => h.hdAssetTypes)
                .Include(h => h.Status)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (hdAssets == null)
            {
                return NotFound();
            }

            return View(hdAssets);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hdAssets = await _context.Assets.FindAsync(id);
            if (hdAssets != null)
            {
                _context.Assets.Remove(hdAssets);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hdAssetsExists(int id)
        {
            return _context.Assets.Any(e => e.AssetID == id);
        }
    }
}

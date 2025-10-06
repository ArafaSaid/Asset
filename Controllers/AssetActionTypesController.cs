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
    public class AssetActionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetActionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssetActionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssetActionTypes.ToListAsync());
        }

        // GET: AssetActionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetActionType = await _context.AssetActionTypes
                .FirstOrDefaultAsync(m => m.ActionTypeId == id);
            if (assetActionType == null)
            {
                return NotFound();
            }

            return View(assetActionType);
        }

        // GET: AssetActionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetActionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActionTypeId,Name,Description")] AssetActionType assetActionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetActionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetActionType);
        }

        // GET: AssetActionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetActionType = await _context.AssetActionTypes.FindAsync(id);
            if (assetActionType == null)
            {
                return NotFound();
            }
            return View(assetActionType);
        }

        // POST: AssetActionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActionTypeId,Name,Description")] AssetActionType assetActionType)
        {
            if (id != assetActionType.ActionTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetActionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetActionTypeExists(assetActionType.ActionTypeId))
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
            return View(assetActionType);
        }

        // GET: AssetActionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetActionType = await _context.AssetActionTypes
                .FirstOrDefaultAsync(m => m.ActionTypeId == id);
            if (assetActionType == null)
            {
                return NotFound();
            }

            return View(assetActionType);
        }

        // POST: AssetActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetActionType = await _context.AssetActionTypes.FindAsync(id);
            if (assetActionType != null)
            {
                _context.AssetActionTypes.Remove(assetActionType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetActionTypeExists(int id)
        {
            return _context.AssetActionTypes.Any(e => e.ActionTypeId == id);
        }
    }
}

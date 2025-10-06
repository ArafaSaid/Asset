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
    public class AssetTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssetTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssetTypes.ToListAsync());
        }

        // GET: AssetTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssetTypes = await _context.AssetTypes
                .FirstOrDefaultAsync(m => m.TypeID == id);
            if (hdAssetTypes == null)
            {
                return NotFound();
            }

            return View(hdAssetTypes);
        }

        // GET: AssetTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeID,Name")] hdAssetTypes hdAssetTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hdAssetTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hdAssetTypes);
        }

        // GET: AssetTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssetTypes = await _context.AssetTypes.FindAsync(id);
            if (hdAssetTypes == null)
            {
                return NotFound();
            }
            return View(hdAssetTypes);
        }

        // POST: AssetTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeID,Name")] hdAssetTypes hdAssetTypes)
        {
            if (id != hdAssetTypes.TypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hdAssetTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hdAssetTypesExists(hdAssetTypes.TypeID))
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
            return View(hdAssetTypes);
        }

        // GET: AssetTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hdAssetTypes = await _context.AssetTypes
                .FirstOrDefaultAsync(m => m.TypeID == id);
            if (hdAssetTypes == null)
            {
                return NotFound();
            }

            return View(hdAssetTypes);
        }

        // POST: AssetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hdAssetTypes = await _context.AssetTypes.FindAsync(id);
            if (hdAssetTypes != null)
            {
                _context.AssetTypes.Remove(hdAssetTypes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hdAssetTypesExists(int id)
        {
            return _context.AssetTypes.Any(e => e.TypeID == id);
        }
    }
}

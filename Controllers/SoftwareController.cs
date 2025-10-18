using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asset.Data;
using Asset.Models;

namespace Asset.Controllers
{
    public class SoftwareController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SoftwareController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Software
        public async Task<IActionResult> Index()
        {
            var software = await _context.Software
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return View(software);
        }

        // GET: Software/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var software = await _context.Software
                .FirstOrDefaultAsync(m => m.SoftwareId == id);
            
            if (software == null)
            {
                return NotFound();
            }

            return View(software);
        }

        // GET: Software/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Software/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoftwareId,Name,Version,Vendor,LicenseKey,LicenseType,NumberOfLicenses,LicensesInUse,PurchaseDate,PurchasePrice,ExpirationDate,RenewalDate,Supplier,Description,Category,SupportContact,IsActive,Notes,CreatedBy")] Software software)
        {
            if (ModelState.IsValid)
            {
                software.CreatedAt = DateTime.UtcNow;
                _context.Add(software);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Software created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(software);
        }

        // GET: Software/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var software = await _context.Software.FindAsync(id);
            if (software == null)
            {
                return NotFound();
            }
            return View(software);
        }

        // POST: Software/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoftwareId,Name,Version,Vendor,LicenseKey,LicenseType,NumberOfLicenses,LicensesInUse,PurchaseDate,PurchasePrice,ExpirationDate,RenewalDate,Supplier,Description,Category,SupportContact,IsActive,Notes,CreatedAt,CreatedBy,UpdatedBy")] Software software)
        {
            if (id != software.SoftwareId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    software.UpdatedAt = DateTime.UtcNow;
                    _context.Update(software);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Software updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareExists(software.SoftwareId))
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
            return View(software);
        }

        // GET: Software/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var software = await _context.Software
                .FirstOrDefaultAsync(m => m.SoftwareId == id);
            
            if (software == null)
            {
                return NotFound();
            }

            return View(software);
        }

        // POST: Software/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var software = await _context.Software.FindAsync(id);
            if (software != null)
            {
                _context.Software.Remove(software);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Software deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SoftwareExists(int id)
        {
            return _context.Software.Any(e => e.SoftwareId == id);
        }
    }
}

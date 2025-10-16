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
            var applicationDbContext = _context.AssetHistories
                .Include(h => h.ActionType)
                .Include(h => h.Asset)
                .Include(h => h.FromStatus)
                .Include(h => h.ToStatus)
                .OrderByDescending(h => h.ActionDate)
                .ThenByDescending(h => h.CreatedAt);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssetHistories/Create
        public IActionResult Create(int? assetId, int? actionTypeId)
        {
            ViewData["ActionTypeId"] = new SelectList(_context.AssetActionTypes, "ActionTypeId", "Name", actionTypeId);
            ViewData["AssetID"] = new SelectList(_context.Assets.Select(a => new { 
                a.AssetID, 
                DisplayText = $"{a.AssetID} - {a.ComputerName} ({a.UserName})" 
            }), "AssetID", "DisplayText", assetId);
            ViewData["FromStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name");
            ViewData["ToStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name");
            
            var model = new hdAssetHistory
            {
                ActionDate = DateTime.Now,
                AssetID = assetId ?? 0,
                ActionTypeId = actionTypeId ?? 0
            };
            
            return View(model);
        }

        // POST: AssetHistories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryID,AssetID,ActionTypeId,Description,FromUser,ToUser,FromLocation,ToLocation,FromStatusId,ToStatusId,SparePart,SparePartSerialNumber,PerformedBy,AssignedToUser,Cost,ReturnReason,AttachmentPath,ActionDate")] hdAssetHistory hdAssetHistory)
        {
            if (ModelState.IsValid)
            {
                hdAssetHistory.CreatedAt = DateTime.UtcNow;
                hdAssetHistory.CreatedBy = User.Identity?.Name ?? "System";
                
                _context.Add(hdAssetHistory);
                
                // Update asset based on action type
                await UpdateAssetFromHistory(hdAssetHistory);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ActionTypeId"] = new SelectList(_context.AssetActionTypes, "ActionTypeId", "Name", hdAssetHistory.ActionTypeId);
            ViewData["AssetID"] = new SelectList(_context.Assets.Select(a => new { 
                a.AssetID, 
                DisplayText = $"{a.AssetID} - {a.ComputerName} ({a.UserName})" 
            }), "AssetID", "DisplayText", hdAssetHistory.AssetID);
            ViewData["FromStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssetHistory.FromStatusId);
            ViewData["ToStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssetHistory.ToStatusId);
            
            return View(hdAssetHistory);
        }

        // Helper method to update asset based on history action
        private async Task UpdateAssetFromHistory(hdAssetHistory history)
        {
            var asset = await _context.Assets.FindAsync(history.AssetID);
            if (asset == null) return;

            switch (history.ActionTypeId)
            {
                case HistoryActionHelper.USER_CHANGE:
                case HistoryActionHelper.DEPLOYMENT:
                case HistoryActionHelper.ASSIGNMENT:
                    if (!string.IsNullOrEmpty(history.AssignedToUser))
                    {
                        asset.UserName = history.AssignedToUser;
                        asset.StatusId = 1; // Active
                    }
                    break;

                case HistoryActionHelper.RETURN_TO_IT:
                    asset.UserName = "IT Department";
                    asset.StatusId = 2; // Inactive
                    break;

                case HistoryActionHelper.RETIRED:
                    asset.StatusId = 4; // Retired
                    break;

                case HistoryActionHelper.MAINTENANCE:
                case HistoryActionHelper.REPAIR:
                    asset.StatusId = 3; // Under Maintenance
                    break;

                case HistoryActionHelper.HARDWARE_ADDITION:
                case HistoryActionHelper.UPGRADE:
                    // Update hardware specs if needed
                    if (!string.IsNullOrEmpty(history.SparePart))
                    {
                        asset.UpdatedAt = DateTime.UtcNow;
                        asset.UpdatedBy = history.PerformedBy;
                    }
                    break;
            }

            asset.UpdatedAt = DateTime.UtcNow;
            asset.UpdatedBy = history.PerformedBy;
            _context.Update(asset);
        }

        // Quick action methods for common operations
        public IActionResult ChangeUser(int assetId)
        {
            return Create(assetId, HistoryActionHelper.USER_CHANGE);
        }

        public IActionResult AddHardware(int assetId)
        {
            return Create(assetId, HistoryActionHelper.HARDWARE_ADDITION);
        }

        public IActionResult ReturnToIT(int assetId)
        {
            return Create(assetId, HistoryActionHelper.RETURN_TO_IT);
        }

        public IActionResult RetireAsset(int assetId)
        {
            return Create(assetId, HistoryActionHelper.RETIRED);
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
                .Include(h => h.FromStatus)
                .Include(h => h.ToStatus)
                .FirstOrDefaultAsync(m => m.HistoryID == id);
            if (hdAssetHistory == null)
            {
                return NotFound();
            }

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
            ViewData["AssetID"] = new SelectList(_context.Assets.Select(a => new { 
                a.AssetID, 
                DisplayText = $"{a.AssetID} - {a.ComputerName} ({a.UserName})" 
            }), "AssetID", "DisplayText", hdAssetHistory.AssetID);
            ViewData["FromStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssetHistory.FromStatusId);
            ViewData["ToStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssetHistory.ToStatusId);
            return View(hdAssetHistory);
        }

        // POST: AssetHistories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryID,AssetID,ActionTypeId,Description,FromUser,ToUser,FromLocation,ToLocation,FromStatusId,ToStatusId,SparePart,SparePartSerialNumber,PerformedBy,AssignedToUser,Cost,ReturnReason,AttachmentPath,ActionDate,CreatedAt,CreatedBy")] hdAssetHistory hdAssetHistory)
        {
            if (id != hdAssetHistory.HistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    hdAssetHistory.UpdatedAt = DateTime.UtcNow;
                    hdAssetHistory.UpdatedBy = User.Identity?.Name ?? "System";
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
            ViewData["AssetID"] = new SelectList(_context.Assets.Select(a => new { 
                a.AssetID, 
                DisplayText = $"{a.AssetID} - {a.ComputerName} ({a.UserName})" 
            }), "AssetID", "DisplayText", hdAssetHistory.AssetID);
            ViewData["FromStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssetHistory.FromStatusId);
            ViewData["ToStatusId"] = new SelectList(_context.AssetStatuses, "StatusId", "Name", hdAssetHistory.ToStatusId);
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
                .Include(h => h.FromStatus)
                .Include(h => h.ToStatus)
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

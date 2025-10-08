using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asset.Data;
using Asset.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcelDataReader;
using System.Data;
using ClosedXML.Excel;

namespace Asset.Controllers
{
    public class AssetExportDto
    {
        public int AssetID { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public string TypeName { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }
        public string Processor { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string OS { get; set; }
        public string Monitor { get; set; }
        public string Printer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? WarrantyExpirationDate { get; set; }
        public string Location { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class AssetImportDto
    {
        public int AssetID { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public string TypeName { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }
        public string Processor { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string OS { get; set; }
        public string Monitor { get; set; }
        public string Printer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? WarrantyExpirationDate { get; set; }
        public string Location { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

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

        public IActionResult Dashboard()
        {
            // You may want to filter or include related data as needed
            var assets = _context.Assets.Include(h => h.hdAssetTypes).Include(h => h.Status).ToList();
            return View(assets);
        }

        [HttpGet]
        public async Task<IActionResult> ExportCsv()
        {
            var assets = await _context.Assets.Include(h => h.hdAssetTypes).Include(h => h.Status).ToListAsync();
            var exportList = assets.Select(a => new AssetExportDto
            {
                AssetID = a.AssetID,
                ComputerName = a.ComputerName,
                UserName = a.UserName,
                TypeName = a.hdAssetTypes?.Name,
                SerialNumber = a.SerialNumber,
                Model = a.Model,
                Manufacturer = a.Manufacturer,
                Supplier = a.Supplier,
                Processor = a.Processor,
                RAM = a.RAM,
                Storage = a.Storage,
                OS = a.OS,
                Monitor = a.Monitor,
                Printer = a.Printer,
                PurchaseDate = a.PurchaseDate,
                PurchasePrice = a.PurchasePrice,
                WarrantyExpirationDate = a.WarrantyExpirationDate,
                Location = a.Location,
                StatusName = a.Status?.Name,
                CreatedAt = a.CreatedAt,
                CreatedBy = a.CreatedBy,
                UpdatedAt = a.UpdatedAt,
                UpdatedBy = a.UpdatedBy
            }).ToList();

            var stream = new MemoryStream();
            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Assets");
                // Header
                var headers = new[] { "AssetID", "ComputerName", "UserName", "TypeName", "SerialNumber", "Model", "Manufacturer", "Supplier", "Processor", "RAM", "Storage", "OS", "Monitor", "Printer", "PurchaseDate", "PurchasePrice", "WarrantyExpirationDate", "Location", "StatusName", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" };
                for (int c = 0; c < headers.Length; c++)
                {
                    ws.Cell(1, c + 1).Value = headers[c];
                    ws.Cell(1, c + 1).Style.Font.Bold = true;
                }

                // Data
                for (int r = 0; r < exportList.Count; r++)
                {
                    var item = exportList[r];
                    int row = r + 2;
                    ws.Cell(row, 1).Value = item.AssetID;
                    ws.Cell(row, 2).Value = item.ComputerName;
                    ws.Cell(row, 3).Value = item.UserName;
                    ws.Cell(row, 4).Value = item.TypeName;
                    ws.Cell(row, 5).Value = item.SerialNumber;
                    ws.Cell(row, 6).Value = item.Model;
                    ws.Cell(row, 7).Value = item.Manufacturer;
                    ws.Cell(row, 8).Value = item.Supplier;
                    ws.Cell(row, 9).Value = item.Processor;
                    ws.Cell(row, 10).Value = item.RAM;
                    ws.Cell(row, 11).Value = item.Storage;
                    ws.Cell(row, 12).Value = item.OS;
                    ws.Cell(row, 13).Value = item.Monitor;
                    ws.Cell(row, 14).Value = item.Printer;
                    ws.Cell(row, 15).Value = item.PurchaseDate;
                    ws.Cell(row, 16).Value = item.PurchasePrice;
                    ws.Cell(row, 17).Value = item.WarrantyExpirationDate;
                    ws.Cell(row, 18).Value = item.Location;
                    ws.Cell(row, 19).Value = item.StatusName;
                    ws.Cell(row, 20).Value = item.CreatedAt;
                    ws.Cell(row, 21).Value = item.CreatedBy;
                    ws.Cell(row, 22).Value = item.UpdatedAt;
                    ws.Cell(row, 23).Value = item.UpdatedBy;
                }

                ws.Columns().AdjustToContents();

                workbook.SaveAs(stream);
            }

            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "assets_export.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> ImportCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ImportError"] = "No file selected.";
                return RedirectToAction("Index");
            }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = file.OpenReadStream())
            {
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                var result = reader.AsDataSet(conf);
                if (result.Tables.Count == 0)
                {
                    TempData["ImportError"] = "No worksheets found in the Excel file.";
                    return RedirectToAction("Index");
                }
                var table = result.Tables[0];

                // Prepare formats for date parsing
                var dateFormats = new[] { "dd/MM/yyyy", "dd/MM/yyyy H:mm", "yyyy-MM-dd", "MM/dd/yyyy" };

                var rowsToAdd = new List<hdAssets>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];
                    // Helper to get cell value by column name (case-insensitive)
                    string Get(string col)
                    {
                        try
                        {
                            var colName = table.Columns.Cast<DataColumn>().FirstOrDefault(c => string.Equals(c.ColumnName, col, StringComparison.OrdinalIgnoreCase));
                            if (colName == null) return null;
                            var val = row[colName]?.ToString();
                            return string.IsNullOrWhiteSpace(val) ? null : val.Trim();
                        }
                        catch
                        {
                            return null;
                        }
                    }

                    var dto = new AssetImportDto
                    {
                        ComputerName = Get("ComputerName") ?? Get("Computer Name") ?? Get("computername"),
                        UserName = Get("UserName") ?? Get("User Name"),
                        TypeName = Get("TypeName") ?? Get("Type Name") ?? Get("Type"),
                        SerialNumber = Get("SerialNumber") ?? Get("Serial Number"),
                        Model = Get("Model"),
                        Manufacturer = Get("Manufacturer"),
                        Supplier = Get("Supplier"),
                        Processor = Get("Processor"),
                        RAM = Get("RAM"),
                        Storage = Get("Storage"),
                        OS = Get("OS") ?? Get("Operating System"),
                        Monitor = Get("Monitor"),
                        Printer = Get("Printer"),
                        Location = Get("Location"),
                        StatusName = Get("StatusName") ?? Get("Status") ?? Get("Status Name"),
                        CreatedBy = Get("CreatedBy") ?? Get("Created By"),
                        UpdatedBy = Get("UpdatedBy") ?? Get("Updated By")
                    };

                    // Parse PurchaseDate
                    var purchaseDateStr = Get("PurchaseDate") ?? Get("Purchase Date");
                    if (!string.IsNullOrEmpty(purchaseDateStr))
                    {
                        if (DateTime.TryParseExact(purchaseDateStr, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var pd))
                            dto.PurchaseDate = pd;
                        else if (DateTime.TryParse(purchaseDateStr, out var pd2))
                            dto.PurchaseDate = pd2;
                        else
                            dto.PurchaseDate = DateTime.MinValue; // or skip/continue based on requirements
                    }
                    else
                    {
                        dto.PurchaseDate = DateTime.MinValue;
                    }

                    // Parse WarrantyExpirationDate
                    var warrantyStr = Get("WarrantyExpirationDate") ?? Get("Warranty Expiration Date");
                    if (!string.IsNullOrEmpty(warrantyStr) && DateTime.TryParseExact(warrantyStr, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var warr))
                    {
                        dto.WarrantyExpirationDate = warr;
                    }

                    // Parse CreatedAt
                    var createdAtStr = Get("CreatedAt") ?? Get("Created At");
                    if (!string.IsNullOrEmpty(createdAtStr) && DateTime.TryParseExact(createdAtStr, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var cat))
                    {
                        dto.CreatedAt = cat;
                    }
                    else
                    {
                        dto.CreatedAt = DateTime.UtcNow;
                    }

                    // Parse UpdatedAt
                    var updatedAtStr = Get("UpdatedAt") ?? Get("Updated At");
                    if (!string.IsNullOrEmpty(updatedAtStr) && DateTime.TryParseExact(updatedAtStr, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var uat))
                    {
                        dto.UpdatedAt = uat;
                    }

                    // Parse PurchasePrice
                    var priceStr = Get("PurchasePrice") ?? Get("Purchase Price");
                    if (!string.IsNullOrEmpty(priceStr) && decimal.TryParse(priceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
                        dto.PurchasePrice = price;

                    // Lookup Type and Status
                    var type = await _context.AssetTypes.FirstOrDefaultAsync(t => t.Name == dto.TypeName);
                    var status = await _context.AssetStatuses.FirstOrDefaultAsync(s => s.Name == dto.StatusName);
                    if (type == null || status == null)
                        continue; // skip row if type/status not found

                    var asset = new hdAssets
                    {
                        ComputerName = dto.ComputerName ?? string.Empty,
                        UserName = dto.UserName ?? string.Empty,
                        TypeID = type.TypeID,
                        SerialNumber = dto.SerialNumber,
                        Model = dto.Model,
                        Manufacturer = dto.Manufacturer,
                        Supplier = dto.Supplier,
                        Processor = dto.Processor,
                        RAM = dto.RAM,
                        Storage = dto.Storage,
                        OS = dto.OS,
                        Monitor = dto.Monitor,
                        Printer = dto.Printer,
                        PurchaseDate = dto.PurchaseDate == DateTime.MinValue ? DateTime.UtcNow.Date : dto.PurchaseDate,
                        PurchasePrice = dto.PurchasePrice,
                        WarrantyExpirationDate = dto.WarrantyExpirationDate,
                        Location = dto.Location,
                        StatusId = status.StatusId,
                        CreatedAt = dto.CreatedAt,
                        CreatedBy = dto.CreatedBy,
                        UpdatedAt = dto.UpdatedAt,
                        UpdatedBy = dto.UpdatedBy
                    };

                    // Avoid duplicates by SerialNumber
                    if (string.IsNullOrEmpty(asset.SerialNumber) || !_context.Assets.Any(a => a.SerialNumber == asset.SerialNumber))
                    {
                        rowsToAdd.Add(asset);
                    }
                }

                if (rowsToAdd.Count > 0)
                {
                    _context.Assets.AddRange(rowsToAdd);
                    await _context.SaveChangesAsync();
                }
            }

            TempData["ImportSuccess"] = "Assets imported successfully.";
            return RedirectToAction("Index");
        }
    }
}

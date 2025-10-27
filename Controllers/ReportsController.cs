using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReportsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var reports = new
            {
                StockReport = await _db.Products
                    .Include(p => p.Category)
                    .OrderBy(p => p.StockQuantity)
                    .ToListAsync(),
                LowStockReport = await _db.Products
                    .Where(p => p.StockQuantity < 5)
                    .Include(p => p.Category)
                    .ToListAsync(),
                CategoryBreakdown = await _db.Products
                    .Include(p => p.Category)
                    .GroupBy(p => p.Category != null ? p.Category.Name : "No Category")
                    .Select(g => new { Category = g.Key, Count = g.Count() })
                    .ToListAsync()
            };

            ViewBag.Reports = reports;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Export()
        {
            var products = await _db.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToListAsync();

            var csv = new System.Text.StringBuilder();
            csv.AppendLine("Name,Category,Price,Stock,SKU");
            foreach (var p in products)
            {
                csv.AppendLine($"\"{p.Name}\",\"{p.Category?.Name ?? ""}\",{p.Price},{p.StockQuantity},\"{p.Sku ?? ""}\"");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "inventory_report.csv");
        }
    }
}

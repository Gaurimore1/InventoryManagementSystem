using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                TotalProducts = await _db.Products.CountAsync(),
                TotalSuppliers = await _db.Suppliers.CountAsync(),
                TotalCategories = await _db.Categories.CountAsync(),
                LowStockProducts = await _db.Products.CountAsync(p => p.StockQuantity < 5),
                OutOfStockProducts = await _db.Products.CountAsync(p => p.StockQuantity == 0)
            };

            ViewBag.Stats = stats;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            var stats = new
            {
                TotalProducts = await _db.Products.CountAsync(),
                TotalSuppliers = await _db.Suppliers.CountAsync(),
                TotalCategories = await _db.Categories.CountAsync(),
                LowStockProducts = await _db.Products.CountAsync(p => p.StockQuantity < 5),
                OutOfStockProducts = await _db.Products.CountAsync(p => p.StockQuantity == 0),
                RecentProducts = await _db.Products
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            ViewBag.Stats = stats;
            return View();
        }
    }
}

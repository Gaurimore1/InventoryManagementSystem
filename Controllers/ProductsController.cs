using System;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

		public ProductsController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		// GET: /Products
        public async Task<IActionResult> Index(int? categoryId, string? q)
		{
            var threshold = HttpContext.RequestServices
                .GetRequiredService<IConfiguration>()
                .GetValue<int>("Inventory:LowStockThreshold", 5);

            var query = _dbContext.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                var term = q.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(term) || (p.Sku != null && p.Sku.ToLower().Contains(term)));
            }

            var products = await query
                .OrderBy(p => p.Name)
                .ToListAsync();

            ViewBag.Categories = await _dbContext.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.LowStockThreshold = threshold;
            return View(products);
		}

		// GET: /Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id.Value);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// GET: /Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
		{
            ViewBag.Categories = _dbContext.Categories.OrderBy(c => c.Name).ToList();
            return View();
		}

		// POST: /Products/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product)
		{
			if (!ModelState.IsValid)
			{
                ViewBag.Categories = _dbContext.Categories.OrderBy(c => c.Name).ToList();
                return View(product);
			}

			_dbContext.Add(product);
			await _dbContext.SaveChangesAsync();
			TempData["Success"] = "Product created successfully.";
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> ExportCsv()
		{
			var rows = await _dbContext.Products
				.Include(p => p.Category)
				.OrderBy(p => p.Name)
				.Select(p => new {
					p.Id, p.Name, p.Sku, p.Price, p.StockQuantity,
					Category = p.Category != null ? p.Category.Name : string.Empty
				})
				.ToListAsync();

			var sb = new System.Text.StringBuilder();
			sb.AppendLine("Id,Name,SKU,Price,Stock,Category");
			foreach (var r in rows)
			{
				string esc(string? s) => (s ?? string.Empty).Replace("\"", "\"\"");
				sb.AppendLine($"{r.Id},\"{esc(r.Name)}\",\"{esc(r.Sku)}\",{r.Price},{r.StockQuantity},\"{esc(r.Category)}\"");
			}
			var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
			return File(bytes, "text/csv", "products.csv");
		}

		// GET: /Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
            var product = await _dbContext.Products.FindAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}
            ViewBag.Categories = _dbContext.Categories.OrderBy(c => c.Name).ToList();
            return View(product);
		}

		// POST: /Products/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
                ViewBag.Categories = _dbContext.Categories.OrderBy(c => c.Name).ToList();
                return View(product);
			}

			try
			{
				_dbContext.Update(product);
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _dbContext.Products.AnyAsync(e => e.Id == product.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			TempData["Success"] = "Product updated successfully.";
			return RedirectToAction(nameof(Index));
		}

		// GET: /Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id.Value);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// POST: /Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = await _dbContext.Products.FindAsync(id);
			if (product != null)
			{
				_dbContext.Products.Remove(product);
				await _dbContext.SaveChangesAsync();
			}
			TempData["Success"] = "Product deleted.";
			return RedirectToAction(nameof(Index));
		}
	}
}



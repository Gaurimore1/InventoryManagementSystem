using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext _db;

		public CategoriesController(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			var items = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
			return View(items);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			if (!ModelState.IsValid) return View(category);
			_db.Add(category);
			await _db.SaveChangesAsync();
			TempData["Success"] = "Category created.";
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var category = await _db.Categories.FindAsync(id);
			if (category == null) return NotFound();
			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Category category)
		{
			if (id != category.Id) return NotFound();
			if (!ModelState.IsValid) return View(category);
			_db.Update(category);
			await _db.SaveChangesAsync();
			TempData["Success"] = "Category updated.";
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int id)
		{
			var category = await _db.Categories.FindAsync(id);
			if (category == null) return NotFound();
			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var category = await _db.Categories.FindAsync(id);
			if (category != null)
			{
				_db.Categories.Remove(category);
				await _db.SaveChangesAsync();
			}
			TempData["Success"] = "Category deleted.";
			return RedirectToAction(nameof(Index));
		}
	}
}



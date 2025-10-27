using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
	public class Product
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;

		[StringLength(500)]
		public string? Description { get; set; }

		[Range(0, 1000000)]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[Range(0, int.MaxValue)]
		public int StockQuantity { get; set; }

		[StringLength(50)]
		public string? Sku { get; set; }

		public int? CategoryId { get; set; }
		public Category? Category { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime? UpdatedAt { get; set; }
	}
}



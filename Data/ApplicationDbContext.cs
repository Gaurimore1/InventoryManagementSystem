using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();

		public override int SaveChanges()
		{
			SetTimestamps();
			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			SetTimestamps();
			return await base.SaveChangesAsync(cancellationToken);
		}

		private void SetTimestamps()
		{
			var entries = ChangeTracker.Entries<Product>();
			var now = DateTime.UtcNow;
			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreatedAt = now;
				}
				if (entry.State == EntityState.Modified)
				{
					entry.Entity.UpdatedAt = now;
				}
			}
		}
	}
}



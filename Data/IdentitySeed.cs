using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
	public static class IdentitySeed
	{
		public static async Task EnsureSeedAsync(IServiceProvider services)
		{
			using var scope = services.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

			string[] roles = new[] { "Admin", "Staff" };
			foreach (var role in roles)
			{
				if (!await roleManager.Roles.AnyAsync(r => r.Name == role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}

			// Demo users
			var adminEmail = "admin@example.com";
			var staffEmail = "staff@example.com";
			var password = "Pass@12345";

			async Task CreateUserIfNotExists(string email, string role)
			{
				var user = await userManager.FindByEmailAsync(email);
				if (user == null)
				{
					user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
					await userManager.CreateAsync(user, password);
				}
				if (!await userManager.IsInRoleAsync(user, role))
				{
					await userManager.AddToRoleAsync(user, role);
				}
			}

			await CreateUserIfNotExists(adminEmail, "Admin");
			await CreateUserIfNotExists(staffEmail, "Staff");

			// Assign Admin role to any existing user for testing
			var allUsers = userManager.Users.ToList();
			foreach (var user in allUsers)
			{
				if (!await userManager.IsInRoleAsync(user, "Admin") && !await userManager.IsInRoleAsync(user, "Staff"))
				{
					await userManager.AddToRoleAsync(user, "Admin");
				}
			}
		}
	}
}



using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
	public class AuthDbContext : IdentityDbContext

	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Seed Roles (User, Admin, SuperAdmin)

			var adminRoleId = "efa8b728-2236-4390-993c-66a4329883d4";
			var superAdminRoleId = "3e115aaa-c4f3-4426-a76f-e8a57fbbe38f";
			var userRoleId = "f13a285f-ed83-42bf-b486-aa438586e361";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name= "Admin",
					NormalizedName = "Admin",
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId
				},
				new IdentityRole
				{
					Name= "SuperAdmin",
					NormalizedName = "SuperAdmin",
					Id = superAdminRoleId,
					ConcurrencyStamp = superAdminRoleId
				},
				new IdentityRole
				{
					Name= "User",
					NormalizedName = "User",
					Id = userRoleId,
					ConcurrencyStamp = userRoleId
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);

			//Seed SuperAdminUser
			var superAdminId = "1e9269c5-1283-41c8-aef8-56cd6a533f06";
			var superAdminUser = new IdentityUser
			{
				UserName = "superadmin@bloggie.com",
				Email = "superadmin@bloggie.com",
				NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
				NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
				Id = superAdminId,
			};

			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
				.HashPassword(superAdminUser, "Superadmin@123");

			builder.Entity<IdentityUser>().HasData(superAdminUser);

			// Add All roles to SuperAdminUser
			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
					RoleId = adminRoleId,
					UserId = superAdminId

				},
				new IdentityUserRole<string>
				{
					RoleId = superAdminRoleId,
					UserId = superAdminId
				},
				new IdentityUserRole<string>
				{
					RoleId= userRoleId,
					UserId= superAdminId
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

		}
	}
}

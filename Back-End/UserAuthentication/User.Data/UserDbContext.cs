using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace User.Infra
{
    public class UserDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            IdentityUser<Guid> admin = CreateAdminUser();
            builder.Entity<IdentityUser<Guid>>().HasData(admin);

            CreateAndAssignRole(builder, admin, "admin");

            CreateAndAssignRole(builder, admin, "Default");
        }

        private IdentityUser<Guid> CreateAdminUser()
        {
            IdentityUser<Guid> admin = new()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid()
            };

            PasswordHasher<IdentityUser<Guid>> hasher = new();

            var password = _configuration.GetSection("AdminPassword").Value;
            admin.PasswordHash = hasher.HashPassword(admin, password);
            return admin;
        }

        private static void CreateAndAssignRole(ModelBuilder builder, IdentityUser<Guid> user, string roleName)
        {
            var role = new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = roleName, NormalizedName = roleName.ToUpper() };

            builder.Entity<IdentityRole<Guid>>().HasData(role);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid> { RoleId = role.Id, UserId = user.Id }
                );
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Service.Core.Storage.Configurations;
using ShopsRUs.Service.Core.Storage.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace ShopsRUs.Service.Core.Storage
{    
    public class ShopsRusDbContext : IdentityDbContext<ShopRusUser, ShopRusRole, string, IdentityUserClaim<string>, ShopRusUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShopsRusDbContext(DbContextOptions<ShopsRusDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Discounts> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ShopRusUserRole>().HasKey(p => new { p.UserId, p.RoleId });

            builder.Entity<ShopRusUserRole>()
            .HasOne(ur => ur.Employee)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId);

            builder.Entity<ShopRusUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            builder.ApplyConfiguration(new ShopRusUserConfiguration());
            builder.ApplyConfiguration(new DiscountsConfiguration());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is TrackedEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            if (!entries.Any()) return;

            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                x.Type == JwtRegisteredClaimNames.Sid);

            var now = DateTimeOffset.Now;

            foreach (var entry in entries)
            {

                if (entry.State == EntityState.Added)
                {
                    ((TrackedEntity)entry.Entity).CreatedOn = now;
                    ((TrackedEntity)entry.Entity).CreatedById = userId?.Value;
                }


                ((TrackedEntity)entry.Entity).ModifiedOn = now;
                ((TrackedEntity)entry.Entity).ModifiedById = userId?.Value;
            }
        }
    }
}

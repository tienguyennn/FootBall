using N.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace N.Model
{
    public class AppDBContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Field> Field { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ServiceFee> ServiceFee { get; set; }
        public DbSet<FieldServiceFee> FieldServiceFee { get; set; }
        public DbSet<ServiceFeePayment> ServiceFeePayment { get; set; }
        public DbSet<FieldArea> FieldArea { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Invite> Invite { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserName = "admin",
                    Type = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin",
                    NormalizedEmail = "ADMIN",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "12345678"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );
            base.OnModelCreating(builder);
        }

    }
}

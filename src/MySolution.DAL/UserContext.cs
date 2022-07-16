using Microsoft.EntityFrameworkCore;
using MySolution.DAL.Entities;

namespace MySolution.DAL
{
    public class UserContext: DbContext
    {
        public DbSet<UserRecord> Users { get; set; }

        public DbSet<UserGroupRecord> UserGroups { get; set; }

        public DbSet<UserStateRecord> UserStates { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRecord>().ToTable("Users")
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<UserGroupRecord>().ToTable("UserGroup");

            modelBuilder.Entity<UserStateRecord>().ToTable("UserState");

            modelBuilder.Entity<UserGroupRecord>().HasData(new UserGroupRecord[]
            {
                new UserGroupRecord {Id = 1, Code = "Admin", Description = "admin"},
                new UserGroupRecord {Id = 2, Code = "User", Description = "User"}
            });

            modelBuilder.Entity<UserStateRecord>().HasData(new UserStateRecord[]
            {
                new UserStateRecord {Id = 1, Code = "Active", Description = "Active"},
                new UserStateRecord {Id = 2, Code = "Blocked", Description = "Blocked"}
            });
        }
    }
}

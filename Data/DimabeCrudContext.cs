
using Microsoft.EntityFrameworkCore;

namespace dimabeCRUD.Data
{
    public class DimabeCrudContext : DbContext
    {
        public DimabeCrudContext (DbContextOptions<DimabeCrudContext> options) : base(options){}
        
        public DbSet<Models.Role> Roles { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.RoleUser> RoleUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Role>().ToTable("Role");
            modelBuilder.Entity<Models.User>().ToTable("User");
            modelBuilder.Entity<Models.RoleUser>().ToTable("RoleUser");
        }
    }
}
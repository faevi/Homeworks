using WebApi2.Models;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace WebApi2.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<User> LoginUsers { get; set; }
        public DbSet<Order> OrderSet { get; set; }
        public DbSet<Stuff> StuffSet { get; set; }
        public DbSet<Value> ValueSet { get; set; }
        public DbSet<Category> CategorySet { get; set; }
        public DbSet<Property> PropertySet { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options )
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(32).IsUnicode(false);
                entity.Property(e => e.Role).HasMaxLength(32).IsUnicode(false);
            });

            modelBuilder.Entity<Stuff>(entity =>
            {
                entity.Property(e => e.Brand).HasMaxLength(150).IsUnicode(false);
                entity.Property(e => e.Seria).HasMaxLength(150).IsUnicode(false);
                entity.Property(e => e.Model).HasMaxLength(150).IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName).HasMaxLength(150).IsUnicode(false);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.Decription).HasMaxLength(1000).IsUnicode(false);
                entity.Property(e => e.PropertyName).HasMaxLength(100).IsUnicode(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

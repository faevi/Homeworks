using System.Data;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace WebApi2.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> LoginUsers { get; set; }
        public DbSet<Order> OrderSet { get; set; }
        public DbSet<Stuff> StuffSet { get; set; }
        public DbSet<Value> ValeuSet { get; set; }
        public DbSet<Category> CategorySet { get; set; }
        public DbSet<Property> PropertySet { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options )
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string adminUsername = "admin";
            string adminPassword = "123456";

            Category category = new Category { CategoryName = "Valenki", Id = 1};
            Stuff stuff = new Stuff { Id = 1, Brand = "Nike", Category_Id = 1, Count = 100, Model = "L", Price = 1000, Seria = "1.0" };
            Stuff stuff1 = new Stuff { Id = 2, Brand = "Nike", Category_Id = 1, Count = 100, Model = "M", Price = 1000, Seria = "1.0" };
            Stuff stuff2 = new Stuff { Id = 3, Brand = "Supremme", Category_Id = 1, Count = 100, Model = "M", Price = 10000, Seria = "1.0" };
            Stuff stuff3 = new Stuff { Id = 4, Brand = "Supremme", Category_Id = 1, Count = 100, Model = "XL", Price = 10000, Seria = "2.0" };

            User adminUser = new User { Id = 1, Username = adminUsername, Password = adminPassword, Role = adminRoleName };
        
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<Category>().HasData(new Category[] { category });
            modelBuilder.Entity<Stuff>().HasData(new Stuff[] { stuff, stuff1, stuff2, stuff3 });
            base.OnModelCreating(modelBuilder);
        }
    }
}

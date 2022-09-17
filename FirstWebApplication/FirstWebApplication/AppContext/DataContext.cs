using FirstWebApplication.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DbSet<DataModel> DataSet { get; set; } = null!;
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
}

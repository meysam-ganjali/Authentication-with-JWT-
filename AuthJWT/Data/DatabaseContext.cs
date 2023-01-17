using AuthJWT.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthJWT.Data;

public class DatabaseContext:DbContext
{

    public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Writer> Writers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new User
            {
                Id = 1, FirstName = "moein", LastName = "fazeli", Username = "admin", Password = "1234",
                Role = "Admin"
            },
            new User
            {
                Id = 2, FirstName = "hassan", LastName = "saeedi", Username = "regularUser", Password = "1234",
                Role = "User"
            }
        });
        base.OnModelCreating(modelBuilder);
    }
}
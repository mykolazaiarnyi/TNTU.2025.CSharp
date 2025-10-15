using Microsoft.EntityFrameworkCore;
using TNTU.ToDoApp.Data.Models;

namespace TNTU.ToDoApp.Data;

public class ToDoContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<ToDoItem> ToDoItems { get; set; }



    //public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    //{
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                .LogTo(Console.WriteLine);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.ToDoItems)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.GreatUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ToDoItem>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
}

using Microsoft.EntityFrameworkCore;

namespace App.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Language> Languages { get; set; }
}


using AgendaContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var contact = modelBuilder.Entity<Contact>();

        contact.Property(item => item.Name).HasMaxLength(120);
        contact.Property(item => item.Phone).HasMaxLength(30);
        contact.Property(item => item.Email).HasMaxLength(160);
        contact.Property(item => item.Category).HasConversion<string>().HasMaxLength(20);

        contact.HasIndex(item => item.Category);
        contact.HasIndex(item => item.Name);
        contact.HasIndex(item => item.Email);
    }
}

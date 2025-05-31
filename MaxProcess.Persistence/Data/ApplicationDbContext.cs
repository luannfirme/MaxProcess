using MaxProcess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaxProcess.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                  .HasColumnType("uniqueidentifier")
                  .IsRequired();

            entity.Property(u => u.Login)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(u => u.Email)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(u => u.Senha)
                  .HasMaxLength(200)
                  .IsRequired();

            entity.HasIndex(u => u.Login)
                  .IsUnique();

            entity.HasIndex(u => u.Email)
                  .IsUnique();

            entity.ToTable("Usuarios");
        });

    }
}

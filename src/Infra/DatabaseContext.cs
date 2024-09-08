using Business.Users.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra;

public partial class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConexao"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07CC19B0EF");

            entity.HasIndex(e => e.Documento, "UQ__Usuarios_Documento").IsUnique();

            entity.Property(e => e.Documento)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SalarioBruto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Setor)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Sobrenome)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

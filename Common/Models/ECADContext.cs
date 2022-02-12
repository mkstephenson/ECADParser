using Common.Models.Data;
using Common.Models.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Common.Models
{
  public class ECADContext : DbContext
  {
    private readonly string _connectionString;

    public ECADContext(string connectionString)
    {
      _connectionString = connectionString;
    }

    public DbSet<CC> CC { get; set; }
    public DbSet<DD> DD { get; set; }
    public DbSet<FG> FG { get; set; }
    public DbSet<FX> FX { get; set; }
    public DbSet<HU> HU { get; set; }
    public DbSet<PP> PP { get; set; }
    public DbSet<QQ> QQ { get; set; }
    public DbSet<RR> RR { get; set; }
    public DbSet<SD> SD { get; set; }
    public DbSet<SS> SS { get; set; }
    public DbSet<TG> TG { get; set; }
    public DbSet<TN> TN { get; set; }
    public DbSet<TX> TX { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Element> Elements { get; set; }
    public DbSet<Source> Sources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Element>().Property(e => e.ElementId).ValueGeneratedNever();
      modelBuilder.Entity<Station>().Property(e => e.StationId).ValueGeneratedNever();
      modelBuilder.Entity<Source>().Property(e => e.SourceId).ValueGeneratedNever();

      modelBuilder.Entity<CC>().IsMemoryOptimized();
      modelBuilder.Entity<DD>().IsMemoryOptimized();
      modelBuilder.Entity<FG>().IsMemoryOptimized();
      modelBuilder.Entity<FX>().IsMemoryOptimized();
      modelBuilder.Entity<HU>().IsMemoryOptimized();
      modelBuilder.Entity<PP>().IsMemoryOptimized();
      modelBuilder.Entity<QQ>().IsMemoryOptimized();
      modelBuilder.Entity<RR>().IsMemoryOptimized();
      modelBuilder.Entity<SD>().IsMemoryOptimized();
      modelBuilder.Entity<SS>().IsMemoryOptimized();
      modelBuilder.Entity<TG>().IsMemoryOptimized();
      modelBuilder.Entity<TN>().IsMemoryOptimized();
      modelBuilder.Entity<TX>().IsMemoryOptimized();
      modelBuilder.Entity<Station>().IsMemoryOptimized();
      modelBuilder.Entity<Element>().IsMemoryOptimized();
      modelBuilder.Entity<Source>().IsMemoryOptimized();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_connectionString);
    }
  }
}

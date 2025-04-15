using MesApiServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MesApiServer.Data;
public class AppDbContext(DbContextOptions<AppDbContext> opt) :DbContext(opt) {
    public DbSet<Device> Devices { get; set; }
    public DbSet<TrackIn> TrackIn { get; set; }
    public DbSet<EQPConfirm> EQPConfirm { get; set; }
    public DbSet<ProcessEnd> ProcessEnd { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Device>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DeviceId)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);
            entity.Property(e => e.LastHeartbeat).IsRequired(true);
        });
    }
}

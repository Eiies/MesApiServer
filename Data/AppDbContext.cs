using MesApiServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MesApiServer.Data;
public class AppDbContext(DbContextOptions<AppDbContext> opt) :DbContext(opt) {
    public DbSet<Device> Devices { get; set; }
    public DbSet<TrackIn> TrackIns { get; set; }
    public DbSet<EQPConfirm> EQPConfirms { get; set; }
    public DbSet<ProcessEnd> ProcessEnds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        // 为 DeviceId 建立唯一索引（DeviceId 已作为主键）
        modelBuilder.Entity<Device>().HasIndex(d => d.DeviceId).IsUnique();

        modelBuilder.Entity<TrackIn>()
            .HasOne(t => t.Device)
            .WithMany(d => d.TrackIns)
            .HasForeignKey(t => t.DeviceId);

        modelBuilder.Entity<EQPConfirm>()
            .HasOne(e => e.Device)
            .WithMany(d => d.EQPConfirms)
            .HasForeignKey(e => e.DeviceId);

        modelBuilder.Entity<ProcessEnd>()
            .HasOne(p => p.Device)
            .WithMany(d => d.ProcessEnds)
            .HasForeignKey(p => p.DeviceId);
    }

}

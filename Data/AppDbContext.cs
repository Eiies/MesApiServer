using MesApiServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MesApiServer.Data;
public class AppDbContext(DbContextOptions<AppDbContext> opt) :DbContext(opt) {
    public DbSet<Device> Devices { get; set; }
    public DbSet<TrackInLog> TrackInLogs { get; set; }
    public DbSet<EQPConfirmLog> EQPConfirmLogs { get; set; }
    public DbSet<ProcessEndLog> ProcessEndLogs { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Device>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DeviceId)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastHeartbeat).IsRequired();
        });
    }
}

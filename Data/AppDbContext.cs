using ApiServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Data {
    public class AppDbContext(DbContextOptions<AppDbContext> opt) :DbContext(opt) {
        public DbSet<Device> Devices { get; set; }
        public DbSet<TrackIn> TrackIns { get; set; }
        public DbSet<EQPConfirm> EQPConfirms { get; set; }
        public DbSet<ProcessEnd> ProcessEnds { get; set; }
        public DbSet<RecordCsvEntity> RecordCsvEntities { get; set; }
        public DbSet<RecordCsvValue> RecordCsvValues { get; set; }

        protected override void OnModelCreating(ModelBuilder m) {
            base.OnModelCreating(m);

            m.Entity<RecordCsvEntity>()
                .HasMany(r => r.Values)
                .WithOne(v => v.RecordCsvEntity)
                .HasForeignKey(v => v.RecordCsvEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            m.Entity<RecordCsvEntity>().HasIndex(r => r.QRCode).IsUnique();

            m.Entity<RecordCsvValue>().Property(v => v.Value).HasColumnType("decimal(10,3)");

            m.Entity<Device>().HasIndex(d => d.DeviceId).IsUnique();

            m.Entity<TrackIn>().HasOne(t => t.Device).WithMany(d => d.TrackIns)
                .HasForeignKey(t => t.DeviceId);

            m.Entity<EQPConfirm>().HasOne(e => e.Device).WithMany(d => d.EQPConfirms)
                .HasForeignKey(e => e.DeviceId);

            m.Entity<ProcessEnd>().HasOne(p => p.Device).WithMany(d => d.ProcessEnds)
                .HasForeignKey(p => p.DeviceId);
        }
    }
}
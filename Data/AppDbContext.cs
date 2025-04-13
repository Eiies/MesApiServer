using MesApiServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MesApiServer.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opt) :DbContext(opt) {
    public DbSet<Device> Device { get; set; }
}

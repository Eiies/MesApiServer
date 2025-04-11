using MesApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace MesApiServer.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options){
    public DbSet<UserDto> Users{ get; set; }
}
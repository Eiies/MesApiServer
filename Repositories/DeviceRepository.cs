using MesApiServer.Data;
using MesApiServer.Models;

namespace MesApiServer.Repositories;

public class DeviceRepository(AppDbContext context, ILogger<DeviceRepository> logger) :IDeviceRepository {

    private readonly AppDbContext _context = context;
    private readonly ILogger<DeviceRepository> _logger = logger;

    public async Task AddDeviceDataAsync(DeviceDto data) {
        _logger.LogDebug("Adding device data for DeviceId: {DeviceId}", data.DeviceId);
        await _context.DeviceDto.AddAsync(data);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Device data added successfully for DeviceId: {DeviceId}", data.DeviceId);
    }
}

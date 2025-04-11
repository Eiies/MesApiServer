using MesApiServer.Models;

namespace MesApiServer.Repositories;
public interface IDeviceRepository {
    Task AddDeviceDataAsync(DeviceDto data);
}


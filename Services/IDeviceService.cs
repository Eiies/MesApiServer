using MesApiServer.Models;

namespace MesApiServer.Services;
public interface IDeviceService {
    Task ProcessDeviceDataAsync(DeviceDto data);
}


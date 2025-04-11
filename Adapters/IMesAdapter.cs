using MesApiServer.Models;

namespace MesApiServer.Adapters;
public interface IMesAdapter {
    Task SendDataToMesAsync(DeviceDto data);
}

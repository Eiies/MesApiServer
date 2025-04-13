using MesApiServer.Adapters;
using MesApiServer.Models;
using MesApiServer.Repositories;

namespace MesApiServer.Services;
public class DeviceService(IDeviceRepository repository, IMesAdapter mesAdapter, ILogger<DeviceService> logger)
        :IDeviceService {
    public void HandleAliveCheck(AliveCheckRequest request) {
        mesAdapter.SendMessage($"Device {request.DeviceId} is alive at {request.Timestamp}");
        repository.SaveHeartbeat(request);
    }

    public void HandleTrackIn(TrackInRequest request) {
        // 发送消息到MES
        mesAdapter.SendMessage(
            $"Device {request.DeviceId} tracked in product {request.ProductCode} at {request.ProductCode}");
        repository.SaveTrackIn(request);
    }

    public void HandleEQPConfirm(EQP2DConfirmRequest request) {
        // 发送消息到MES
        mesAdapter.SendMessage(
            $"Device {request.DeviceId} confirmed barcode {request.Barcode} at {request.Barcode}");
        repository.SaveEQPConfirm(request);
    }

    public void HandleProcessEnd(ProcessEndRequest request) {
        // 发送消息到MES
        mesAdapter.SendMessage(
            $"Device {request.DeviceId} process ended with result {request.Result} at {request.Result}");
        repository.SaveProcessEnd(request);
    }

}
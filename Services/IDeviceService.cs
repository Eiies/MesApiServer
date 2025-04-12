using MesApiServer.Models;

namespace MesApiServer.Services;
public interface IDeviceService {
    Task ProcessDeviceDataAsync(DeviceDto data);


    void HandleAliveCheck(AliveCheckRequest request);
    void HandleTrackIn(TrackInRequest request);
    void HandleEQPConfirm(EQP2DConfirmRequest request);
    void HandleProcessEnd(ProcessEndRequest request);

}


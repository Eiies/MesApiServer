using MesApiServer.Models;

namespace MesApiServer.Repositories;
public interface IDeviceRepository {
    void SaveEQPConfirm(EQP2DConfirmRequest request);
    void SaveHeartbeat(AliveCheckRequest request);
    void SaveProcessEnd(ProcessEndRequest request);
    void SaveTrackIn(TrackInRequest request);
}


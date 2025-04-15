using MesApiServer.Adapters;
using MesApiServer.Data.Entities;
using MesApiServer.Models;
using MesApiServer.Repositories;

namespace MesApiServer.Services;

public class DeviceService(
        IDeviceRepository deviceRepository,
        IMesAdapter mesAdapter
    ) :IDeviceService {

    public void HandleAliveCheck(AliveCheckRequest request) {
        var eqpID = request.Context.EQPID.Trim().ToUpperInvariant(); // 处理 EQPID 字段
        var entity = new Device {
            DeviceId = eqpID,
            LastHeartbeat = request.StartTime,
        };
        deviceRepository.SaveHeartbeat(entity);
        mesAdapter.SendAliveNotification(request);
    }

    public void HandleTrackIn(TrackInRequest request) {
        if(string.IsNullOrWhiteSpace(request.Content.EQPID) || string.IsNullOrWhiteSpace(request.Content.LotID))
            throw new ArgumentException("EQPID 和 LotID 是必填项");

        request.Content.EQPID = request.Content.EQPID.Trim().ToUpperInvariant();
        deviceRepository.SaveTrackIn(request);
        mesAdapter.SendMessage($"设备 {request.Content.EQPID} 入库产品 {request.Content.LotID}");
    }

    public void HandleEQPConfirm(EQP2DConfirmRequest request) {
        if(string.IsNullOrWhiteSpace(request.DeviceId) || string.IsNullOrWhiteSpace(request.Barcode))
            throw new ArgumentException("DeviceId 和 Barcode 是必填项");

        request.DeviceId = request.DeviceId.Trim().ToUpperInvariant();
        deviceRepository.SaveEQPConfirm(request);
        mesAdapter.SendMessage($"设备 {request.DeviceId} 扫描确认条码 {request.Barcode}，时间：{request.ScanTime}");
    }

    public void HandleProcessEnd(ProcessEndRequest request) {
        if(string.IsNullOrWhiteSpace(request.DeviceId))
            throw new ArgumentException("DeviceId 是必填项");

        request.DeviceId = request.DeviceId.Trim().ToUpperInvariant();
        deviceRepository.SaveProcessEnd(request);
        mesAdapter.SendMessage($"设备 {request.DeviceId} 完成工序，结果：{request.Result}，时间：{request.EndTime}");
    }
}


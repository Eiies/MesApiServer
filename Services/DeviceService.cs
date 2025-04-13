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
        if(string.IsNullOrWhiteSpace(request.DeviceId))
            throw new ArgumentException("DeviceId 不能为空");
        if(request.Timestamp == default)
            throw new ArgumentException("Timestamp 无效");

        request.DeviceId = request.DeviceId.Trim().ToUpperInvariant();
        var entity = new Device {
            DeviceId = request.DeviceId,
            LastHeartbeat = request.Timestamp
        };
        deviceRepository.SaveHeartbeat(entity);
        mesAdapter.SendAliveNotification(request);
    }

    public void HandleTrackIn(TrackInRequest request) {
        if(string.IsNullOrWhiteSpace(request.DeviceId) || string.IsNullOrWhiteSpace(request.ProductCode))
            throw new ArgumentException("DeviceId 和 ProductCode 是必填项");

        request.DeviceId = request.DeviceId.Trim().ToUpperInvariant();
        deviceRepository.SaveTrackIn(request);
        mesAdapter.SendMessage($"设备 {request.DeviceId} 入库产品 {request.ProductCode}，操作员：{request.Operator}，时间：{request.StartTime}");
    }

    public void HandleEQPConfirm(EQP2DConfirmRequest request) {
        if(string.IsNullOrWhiteSpace(request.DeviceId) || string.IsNullOrWhiteSpace(request.Barcode))
            throw new ArgumentException("DeviceId 和 Barcode 是必填项");

        request.DeviceId = request.DeviceId.Trim().ToUpperInvariant();
        deviceRepository.SaveEQPConfirm(request);
        mesAdapter.SendMessage($"设备 {request.DeviceId} 扫描确认条码 {request.Barcode}，操作员：{request.Operator}，时间：{request.ScanTime}");
    }

    public void HandleProcessEnd(ProcessEndRequest request) {
        if(string.IsNullOrWhiteSpace(request.DeviceId))
            throw new ArgumentException("DeviceId 是必填项");

        request.DeviceId = request.DeviceId.Trim().ToUpperInvariant();
        deviceRepository.SaveProcessEnd(request);
        mesAdapter.SendMessage($"设备 {request.DeviceId} 完成工序，结果：{request.Result}，时间：{request.EndTime}");
    }
}


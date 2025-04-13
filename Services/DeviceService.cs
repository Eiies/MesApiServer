using MesApiServer.Adapters;
using MesApiServer.Data.Entities;
using MesApiServer.Models;
using MesApiServer.Repositories;

namespace MesApiServer.Services;
public class DeviceService(IDeviceRepository deviceRepository, IMesAdapter mesAdapter, ILogger<DeviceService> logger)
        :IDeviceService {
    public void HandleAliveCheck(AliveCheckRequest request) {
        // 数据校验：确保 DeviceId 不为空，Timestamp 合理
        if(string.IsNullOrWhiteSpace(request.DeviceId)) {
            logger.LogWarning("收到无效的心跳请求：DeviceId 为空。");
            throw new ArgumentException("DeviceId 为必填项");
        }
        if(request.Timestamp == default) {
            logger.LogWarning("收到无效的心跳请求：Timestamp 无效。");
            throw new ArgumentException("Timestamp 无效");
        }

        // 数据标准化：例如统一设备编号格式
        string standardizedDeviceId = request.DeviceId.Trim().ToUpperInvariant();
        request.DeviceId = standardizedDeviceId;

        logger.LogDebug($"标准化后的设备编号：{standardizedDeviceId}");

        // 映射到数据库实体（Device 实体）
        var deviceEntity = new Device {
            DeviceId = standardizedDeviceId,
            LastHeartbeat = request.Timestamp
        };

        // 保存设备心跳数据到数据库（Repository 层处理增/改逻辑）
        deviceRepository.SaveHeartbeat(deviceEntity);

        // 将数据转发给 MES 系统（通过适配器实现松耦合）
        mesAdapter.SendAliveNotification(request);
    }

    public void HandleTrackIn(TrackInRequest request) {
        // 发送消息到MES
        mesAdapter.SendMessage(
            $"Device {request.DeviceId} tracked in product {request.ProductCode} at {request.ProductCode}");
        deviceRepository.SaveTrackIn(request);
    }

    public void HandleEQPConfirm(EQP2DConfirmRequest request) {
        // 发送消息到MES
        mesAdapter.SendMessage(
            $"Device {request.DeviceId} confirmed barcode {request.Barcode} at {request.Barcode}");
        deviceRepository.SaveEQPConfirm(request);
    }

    public void HandleProcessEnd(ProcessEndRequest request) {
        // 发送消息到MES
        mesAdapter.SendMessage(
            $"Device {request.DeviceId} process ended with result {request.Result} at {request.Result}");
        deviceRepository.SaveProcessEnd(request);
    }

}
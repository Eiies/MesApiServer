using MesApiServer.Data;
using MesApiServer.Data.Entities;
using MesApiServer.Models;

namespace MesApiServer.Repositories;
public class DeviceRepository(AppDbContext context, ILogger<DeviceRepository> logger) :IDeviceRepository {
    public void SaveEQPConfirm(EQP2DConfirmRequest request) {
        var log = new EQPConfirmLog {
            DeviceId = request.DeviceId,
            Barcode = request.Barcode,
            ScanTime = request.ScanTime
        };
        context.EQPConfirmLogs.Add(log);
        context.SaveChanges();
        logger.LogInformation("【设备确认】设备 {DeviceId} 确认记录已保存", request.DeviceId);
    }

    public void SaveHeartbeat(Device device) {
        var existingDevice = context.Devices.FirstOrDefault(d => d.DeviceId == device.DeviceId);
        if(existingDevice == null) {
            // 如果设备不存在，则添加新设备
            context.Devices.Add(device);
            logger.LogInformation("【心跳】设备 {DeviceId} 新设备已添加，时间：{Timestamp}", device.DeviceId, device.LastHeartbeat);
        } else {
            // 如果设备存在，则更新心跳时间
            existingDevice.LastHeartbeat = device.LastHeartbeat;
            context.Devices.Update(existingDevice);
            logger.LogInformation("【心跳】设备 {DeviceId} 心跳时间已更新，时间：{Timestamp}", device.DeviceId, device.LastHeartbeat);
        }
        context.SaveChanges();
    }

    public void SaveProcessEnd(ProcessEndRequest request) {
        var log = new ProcessEndLog {
            DeviceId = request.DeviceId,
            Result = request.Result,
            EndTime = request.EndTime
        };
        context.ProcessEndLogs.Add(log);
        context.SaveChanges();
        logger.LogInformation("【工序结束】设备 {DeviceId} 结束记录已保存", request.DeviceId);
    }

    public void SaveTrackIn(TrackInRequest request) {
        var log = new TrackInLog {
            DeviceId = request.DeviceId,
            ProductCode = request.ProductCode,
            StartTime = request.StartTime
        };
        context.TrackInLogs.Add(log);
        context.SaveChanges();
        logger.LogInformation("【入库】设备 {DeviceId} 入库记录已保存", request.DeviceId);
    }

}
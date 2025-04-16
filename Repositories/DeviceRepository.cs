using MesApiServer.Data;
using MesApiServer.Data.Entities;
using MesApiServer.Models;

namespace MesApiServer.Repositories;

public class DeviceRepository(AppDbContext context, ILogger<DeviceRepository> logger) :IDeviceRepository {

    public void SaveHeartbeat(Device device) {
        var existingDevice = context.Devices.FirstOrDefault(d => d.DeviceId == device.DeviceId);
        if(existingDevice == null) {
            context.Devices.Add(device);
            logger.LogInformation("【心跳】新增设备 {DeviceId}，时间：{Timestamp}", device.DeviceId, device.LastHeartbeat);
        } else {
            existingDevice.LastHeartbeat = device.LastHeartbeat;
            context.Devices.Update(existingDevice);
            logger.LogInformation("【心跳】更新设备 {DeviceId} 心跳时间为 {Timestamp}", device.DeviceId, device.LastHeartbeat);
        }
        context.SaveChanges();
    }

    public void SaveTrackIn(TrackInRequest r) {
        // 确保设备存在，不存在则新增（心跳方法也可调用此方法）  
        var device = EnsureDeviceExists(r.Content.EQPID);
        var trackIn = new TrackIn {
            DeviceId = device.DeviceId, // 使用设备编号作为外键
            LotID = r.Content.LotID,
            CarrierID = r.Content.CarrierID,
            EmployeeID = r.Content.EmployeeID,
            TrackTime = DateTime.Parse(r.DateTime)

        };
        context.TrackIns.Add(trackIn);
        context.SaveChanges();
        logger.LogInformation($"【入库】设备 {r.Content.EQPID} 入库记录已保存，批次号：{r.Content.LotID}");
    }

    public void SaveEQPConfirm(EQP2DConfirmRequest r) {
        var device = EnsureDeviceExists(r.Content.EQPID);
        var confirm = new EQPConfirm {
            DeviceId = device.DeviceId,
            LotID = r.Content.LotID,
            EQP2DID = r.Content.EQP2DID,
            Orientation = r.Content.Orientation,
            RotationAngle = r.Content.RotationAngle,
            ConnectMode = r.Content.ConnectMode,
            ScanTime = DateTime.Parse(r.DateTime)
        };
        context.EQPConfirms.Add(confirm);
        context.SaveChanges();
        logger.LogInformation($"【设备确认】设备 {r.Content.EQPID} 确认记录已保存，条码：{r.Content.EQP2DID}");
    }

    public void SaveProcessEnd(ProcessEndRequest r) {
        var device = EnsureDeviceExists(r.Content.EQPID);
        var processEnd = new ProcessEnd {
            DeviceId = device.DeviceId,
            CarrierID = r.Content.CarrierID,
            LotID = r.Content.LotID,
            EndTime = DateTime.Parse(r.DateTime),

        };
        context.ProcessEnds.Add(processEnd);
        context.SaveChanges();
        logger.LogInformation($"【工序结束】设备 {r.Content.EQPID} 工序结束记录已保存");
    }

    /// <summary>
    /// 根据设备编号确保设备存在，如不存在则新增设备记录
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns></returns>
    private Device EnsureDeviceExists(string deviceId) {
        var device = context.Devices.FirstOrDefault(d => d.DeviceId == deviceId);
        if(device == null) {
            device = new Device {
                DeviceId = deviceId,
                LastHeartbeat = DateTime.UtcNow
            };
            context.Devices.Add(device);
            context.SaveChanges();
            logger.LogInformation("【设备检查】设备 {DeviceId} 不存在，新增记录。", deviceId);
        }
        return device;
    }
}

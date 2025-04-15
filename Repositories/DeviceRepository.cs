using MesApiServer.Data;
using MesApiServer.Data.Entities;
using MesApiServer.Models;

namespace MesApiServer.Repositories;
public class DeviceRepository(AppDbContext context, ILogger<DeviceRepository> logger) :IDeviceRepository {
    public void SaveEQPConfirm(EQP2DConfirmRequest request) {
        var log = new EQPConfirm {
            EQPID = request.DeviceId,
            Barcode = request.Barcode,
            ScanTime = request.ScanTime,
        };
        context.EQPConfirm.Add(log);
        context.SaveChanges();
        logger.LogInformation("【设备确认】设备 {DeviceId} 确认记录已保存", request.DeviceId);
    }

    public void SaveHeartbeat(Device device) {
        var existingDevice = context.Devices.FirstOrDefault(d => d.DeviceId == device.DeviceId);
        if(existingDevice == null) {
            context.Devices.Add(device);
            logger.LogInformation("【心跳】新增设备 {DeviceId}，时间：{Timestamp}", device.DeviceId, device.LastHeartbeat);
        } else {
            existingDevice.LastHeartbeat = device.LastHeartbeat;
            context.Devices.Update(existingDevice);
            logger.LogInformation("【心跳】更新设备 {DeviceId}，时间：{Timestamp}", device.DeviceId, device.LastHeartbeat);
        }
        context.SaveChanges();
    }

    public void SaveProcessEnd(ProcessEndRequest request) {
        var log = new ProcessEnd {
            EQPID = request.DeviceId,
            Result = request.Result,
            EndTime = request.EndTime,
            Operator = request.Operator

        };
        context.ProcessEnd.Add(log);
        context.SaveChanges();
        logger.LogInformation("【工序结束】设备 {DeviceId} 结束记录已保存", request.DeviceId);
    }

    public void SaveTrackIn(TrackInRequest request) {
        var t = new TrackIn {
            EQPID = request.Content.EQPID,
            LotID = request.Content.LotID,
            CarrierID = request.Content.CarrierID,
            EmployeeID = request.Content.EmployeeID,
        };
        context.TrackIn.Add(t);
        context.SaveChanges();
        logger.LogInformation("【入库】设备 {DeviceId} 入库记录已保存", request.Content.EQPID);
    }

}
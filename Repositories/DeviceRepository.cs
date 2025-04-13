using MesApiServer.Data;
using MesApiServer.Models;

namespace MesApiServer.Repositories;
public class DeviceRepository(AppDbContext context, ILogger<DeviceRepository> logger) :IDeviceRepository {
    public void SaveEQPConfirm(EQP2DConfirmRequest request) {
        // TODO: 暂时不实现，使用 log 代替
        logger.LogInformation("【设备确认】设备 {DeviceId} 确认请求已保存，时间：{Timestamp}", request.DeviceId, request.ScanTime);
    }

    public void SaveHeartbeat(AliveCheckRequest request) {
        // TODO: 暂时不实现，使用 log 代替
        logger.LogInformation("【心跳】设备 {DeviceId} 心跳请求已保存，时间：{Timestamp}", request.DeviceId, request.Timestamp);
    }

    public void SaveProcessEnd(ProcessEndRequest request) {
        // TODO: 暂时不实现，使用 log 代替
        logger.LogInformation("【工序结束】设备 {DeviceId} 工序结束请求已保存，时间：{Timestamp}", request.DeviceId, request.EndTime);
    }

    public void SaveTrackIn(TrackInRequest request) {
        // TODO: 暂时不实现，使用 log 代替
        logger.LogInformation("【入库】设备 {DeviceId} 入库请求已保存，时间：{Timestamp}", request.DeviceId, request.StartTime);
    }

}
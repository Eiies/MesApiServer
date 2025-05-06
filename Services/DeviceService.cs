using ApiServer.Adapters;
using ApiServer.Data.Entities;
using ApiServer.Models;
using ApiServer.Repositories;

namespace ApiServer.Services;

public interface IDeviceService {
    void HandleAliveCheck(AliveCheckRequest request);
    void HandleTrackIn(TrackInRequest request);
    void HandleEQPConfirm(EQP2DConfirmRequest request);
    void HandleProcessEnd(ProcessEndRequest request);
}

public class DeviceService(IDeviceRepository deviceRepository, IMesAdapter mesAdapter, ILogger<DeviceService> logger) :IDeviceService {
    public void HandleAliveCheck(AliveCheckRequest r) {
        // 校验和标准化
        if(string.IsNullOrWhiteSpace(r.Content.EQPID)) {
            throw new ArgumentException("EQPID 不能为空");
        }

        if(string.IsNullOrWhiteSpace(r.DateTime) || !DateTime.TryParse(r.DateTime, out _)) {
            throw new ArgumentException("StatueTimes 不能为空");
        }

        string deviceId = r.Content.EQPID.Trim().ToUpperInvariant();

        // 构造 Device 实体并保存心跳（使用传入的 StatueTimes 转换为 DateTime，可根据具体格式转换）
        if(!DateTime.TryParse(r.DateTime, out DateTime heartbeatTime)) {
            heartbeatTime = DateTime.UtcNow;
        }

        Device device = new() {
            DeviceId = deviceId,
            LastHeartbeat = heartbeatTime
        };

        deviceRepository.SaveHeartbeat(device);

        // 转发 MQTT 消息给 MES（调用适配器）
        mesAdapter.SendAliveNotification(new AliveCheckRequest {
            From = r.From,
            DateTime = r.DateTime,
            Content = new AliveCheckRequest.AliveCheckContext { EQPID = deviceId }
        });
    }

    public void HandleTrackIn(TrackInRequest r) {
        if(string.IsNullOrWhiteSpace(r.Content.EQPID)) {
            throw new ArgumentException("EQPID 不能为空");
        }

        // 标准化设备编号
        r.Content.EQPID = r.Content.EQPID.Trim().ToUpperInvariant();

        deviceRepository.SaveTrackIn(r);
        mesAdapter.SendMessage(
            $"设备 {r.Content.EQPID} 批次 {r.Content.LotID}，操作员：{r.Content.EmployeeID}，时间：{r.DateTime}");
    }

    public void HandleEQPConfirm(EQP2DConfirmRequest r) {
        r.Content.EQPID = r.Content.EQPID.Trim().ToUpperInvariant();

        deviceRepository.SaveEQPConfirm(r);
        mesAdapter.SendMessage($"设备 {r.Content.EQPID} 条码 {r.Content.EQP2DID}，批次：{r.Content.LotID}，时间：{r.DateTime}");
    }

    public void HandleProcessEnd(ProcessEndRequest r) {
        r.Content.EQPID = r.Content.EQPID.Trim().ToUpperInvariant();

        deviceRepository.SaveProcessEnd(r);
        mesAdapter.SendMessage(
            $"设备 {r.Content.EQPID} 完成工序，批次：{r.Content.LotID}载具：{r.Content.CarrierID},时间：{r.DateTime}");
    }
}

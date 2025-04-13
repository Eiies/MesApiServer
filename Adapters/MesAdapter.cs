using MesApiServer.Models;
using System.Text.Json;

namespace MesApiServer.Adapters;

public class MesAdapter(ILogger<MesAdapter> logger) :IMesAdapter {
    public void SendAliveNotification(AliveCheckRequest request) {

        var message = JsonSerializer.Serialize(new {
            Type = "Heartbeat",
            request.DeviceId,
            request.Timestamp
        });
        logger.LogDebug("[MES] 心跳通知发送：{Message}", message);
    }

    // 发送消息到MES
    public void SendMessage(string message) {
        // TODO: 暂时使用日志记录消息，实际应用中应替换为发送到MES的逻辑
        logger.LogDebug($"【MesAdapter】 MES: {message}");
    }

    public void SendTrackInNotification(TrackInRequest request) {
        var message = JsonSerializer.Serialize(new {
            Type = "TrackIn",
            DeviceId = request.DeviceId,
            ProductCode = request.ProductCode,
            Operator = request.Operator,
            StartTime = request.StartTime
        });
        logger.LogInformation("[MES] 入库通知发送：{Message}", message);
    }

    public void SendEQPConfirmNotification(EQP2DConfirmRequest request) {
        var message = JsonSerializer.Serialize(new {
            Type = "EQP2DConfirm",
            DeviceId = request.DeviceId,
            Barcode = request.Barcode,
            ScanTime = request.ScanTime
        });
        logger.LogInformation("[MES] 2D确认通知发送：{Message}", message);
    }

    public void SendProcessEndNotification(ProcessEndRequest request) {
        var message = JsonSerializer.Serialize(new {
            Type = "ProcessEnd",
            DeviceId = request.DeviceId,
            Result = request.Result,
            EndTime = request.EndTime
        });
        logger.LogInformation("[MES] 工序结束通知发送：{Message}", message);
    }

}

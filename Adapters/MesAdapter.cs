using MesApiServer.Models;

namespace MesApiServer.Adapters;

public class MesAdapter(ILogger<MesAdapter> logger) :IMesAdapter {
    public void SendAliveNotification(AliveCheckRequest request) {
        logger.LogInformation($"【MES适配器】转发设备 {request.DeviceId} 心跳数据给 MES 系统，时间：{request.Timestamp}");
    }

    // 发送消息到MES
    public void SendMessage(string message) {
        // TODO: 暂时使用日志记录消息，实际应用中应替换为发送到MES的逻辑
        logger.LogInformation($"Sending message to MES: {message}");
    }
}

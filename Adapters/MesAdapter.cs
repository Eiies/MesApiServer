namespace MesApiServer.Adapters;
public class MesAdapter(ILogger<MesAdapter> logger) :IMesAdapter {
    // 发送消息到MES
    public void SendMessage(string message) {
        // TODO: 暂时使用日志记录消息，实际应用中应替换为发送到MES的逻辑
        logger.LogInformation($"Sending message to MES: {message}");
    }
}

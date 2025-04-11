using MesApiServer.Models;

namespace MesApiServer.Adapters;
public class MesAdapter(ILogger<MesAdapter> logger) :IMesAdapter {
    private readonly ILogger<MesAdapter> _logger = logger;

    public async Task SendDataToMesAsync(DeviceDto data) {
        // 示例：调用 MES 接口或消息队列，此处仅模拟日志输出
        await Task.Run(() => {
            _logger.LogInformation("【MES适配器】设备 {DeviceId} 数据已转发，时间：{Timestamp}", data.DeviceId, data.Timestamp);
        });
    }
}


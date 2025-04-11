using MesApiServer.Adapters;
using MesApiServer.Models;
using MesApiServer.Repositories;

namespace MesApiServer.Services;

public class DeviceService(IDeviceRepository repository, IMesAdapter mesAdapter, ILogger<DeviceService> logger) :IDeviceService {
    private readonly IDeviceRepository _repository = repository;
    private readonly IMesAdapter _mesAdapter = mesAdapter;
    private readonly ILogger<DeviceService> _logger = logger;

    public async Task ProcessDeviceDataAsync(DeviceDto data) {
        _logger.LogDebug("Processing data for DeviceId: {DeviceId}", data.DeviceId);
        // 数据标准化处理（示例：简单校验）
        if(string.IsNullOrEmpty(data.DeviceId) || string.IsNullOrEmpty(data.DataPayload)) {
            _logger.LogWarning("Data validation failed for DeviceId: {DeviceId}", data.DeviceId);
            throw new ArgumentException("数据不完整");
        }

        // 存储到数据库
        await _repository.AddDeviceDataAsync(data);

        // 调用适配器转发 MES 系统
        await _mesAdapter.SendDataToMesAsync(data);
        _logger.LogInformation("Data processing completed for DeviceId: {DeviceId}", data.DeviceId);
    }
}

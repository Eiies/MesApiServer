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


    public void HandleAliveCheck(AliveCheckRequest request) {
        _logger.LogInformation($"[Heartbeat] Device: {request.DeviceId}, Time: {request.Timestamp}");
        _repository.SaveHeartbeat(request);
    }

    public void HandleTrackIn(TrackInRequest request) {
        _logger.LogInformation($"[TrackIn] Device: {request.DeviceId}, Product: {request.ProductCode}");
        _repository.SaveTrackIn(request);
    }

    public void HandleEQPConfirm(EQP2DConfirmRequest request) {
        _logger.LogInformation($"[2D Confirm] Device: {request.DeviceId}, Barcode: {request.Barcode}");
        _repository.SaveEQPConfirm(request);
    }

    public void HandleProcessEnd(ProcessEndRequest request) {
        _logger.LogInformation($"[ProcessEnd] Device: {request.DeviceId}, Result: {request.Result}");
        _repository.SaveProcessEnd(request);
    }
}

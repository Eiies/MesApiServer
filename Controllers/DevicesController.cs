using MesApiServer.Models;
using MesApiServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesApiServer.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DevicesController(IDeviceService deviceService, ILogger<DevicesController> logger) :ControllerBase {
    private readonly IDeviceService _deviceService = deviceService;
    private readonly ILogger<DevicesController> _logger = logger;

    // POST api/devices
    [HttpPost]
    public async Task<IActionResult> UploadDeviceData([FromBody] DeviceDto data) {
        if(data == null) {
            _logger.LogWarning("Upload attempt with null data");
            return BadRequest("数据为空");
        }

        try {
            await _deviceService.ProcessDeviceDataAsync(data);
            return Ok(new { status = "success", message = "数据上传成功" });
        } catch(Exception ex) {
            _logger.LogError(ex, "Error processing device data for DeviceId: {DeviceId}", data.DeviceId);
            return StatusCode(500, new { status = "error", message = ex.Message });
        }
    }
}


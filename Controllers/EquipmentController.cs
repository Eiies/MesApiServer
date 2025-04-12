using Microsoft.AspNetCore.Mvc;
using MesApiServer.Models;
using MesApiServer.Services;

namespace MesApiServer.Controllers;

[ApiController]
[Route("[controller]")]
public class EquipmentController :ControllerBase {
    private readonly IDeviceService _deviceService;
    private readonly ILogger<EquipmentController> _logger;

    public EquipmentController(IDeviceService deviceService, ILogger<EquipmentController> logger) {
        _deviceService = deviceService;
        _logger = logger;
    }

    [HttpPost("AliveCheck")]
    public IActionResult AliveCheck([FromBody] AliveCheckRequest request) {
        _deviceService.HandleAliveCheck(request);
        return Ok(new { message = "Heartbeat received" });
    }

    [HttpPost("TrackInRequest")]
    public IActionResult TrackInRequest([FromBody] TrackInRequest request) {
        _deviceService.HandleTrackIn(request);
        return Ok(new { message = "TrackIn processed" });
    }

    [HttpPost("EQP2DComfirm")]
    public IActionResult EQP2DComfirm([FromBody] EQP2DConfirmRequest request) {
        _deviceService.HandleEQPConfirm(request);
        return Ok(new { message = "2D confirm received" });
    }

    [HttpPost("ProcessEnd")]
    public IActionResult ProcessEnd([FromBody] ProcessEndRequest request) {
        _deviceService.HandleProcessEnd(request);
        return Ok(new { message = "Process end recorded" });
    }
}

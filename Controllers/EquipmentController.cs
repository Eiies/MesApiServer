using MesApiServer.Models;
using MesApiServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesApiServer.Controllers;
[ApiController]
[Route("[controller]")]
public class EquipmentController(IDeviceService deviceService, ILogger<EquipmentController> logger)
        :ControllerBase {
    [HttpPost("AliveCheck")]
    public IActionResult AliveCheck([FromBody] AliveCheckRequest request) {
        deviceService.HandleAliveCheck(request);
        logger.LogInformation("心跳指令收到");
        return Ok(new { message = "心跳指令收到" });
    }

    [HttpPost("TrackInRequest")]
    public IActionResult TrackInRequest([FromBody] TrackInRequest request) {
        deviceService.HandleTrackIn(request);
        return Ok(new { message = "TrackIn processed" });
    }

    [HttpPost("EQP2DComfirm")]
    public IActionResult EQP2DComfirm([FromBody] EQP2DConfirmRequest request) {
        deviceService.HandleEQPConfirm(request);
        return Ok(new { message = "2D confirm received" });
    }

    [HttpPost("ProcessEnd")]
    public IActionResult ProcessEnd([FromBody] ProcessEndRequest request) {
        deviceService.HandleProcessEnd(request);
        return Ok(new { message = "Process end recorded" });
    }
}

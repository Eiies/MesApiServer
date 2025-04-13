using MesApiServer.Models;
using MesApiServer.Services;
using MesApiServer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MesApiServer.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EquipmentController(IDeviceService deviceService, ILogger<EquipmentController> logger)
        :ControllerBase {

    /// <summary>
    /// 设备心跳接口
    /// POST: /api/Equipment/AliveCheck
    /// </summary>
    [HttpPost("AliveCheck")]
    public IActionResult AliveCheck([FromBody] AliveCheckRequest request) {
        try {
            deviceService.HandleAliveCheck(request);
            return Ok(new { message = "Alive check processed" });
        } catch(Exception ex) {
            logger.LogError(ex, "处理心跳请求时发生错误");
            return BadRequest(new { message = "Error processing alive check" });
        }
    }

    /// <summary>
    /// 上机请求接口
    /// POST: /api/Equipment/TrackInRequest
    /// </summary>
    [HttpPost("TrackInRequest")]
    public IActionResult TrackInRequest([FromBody] TrackInRequest request) {
        try {
            deviceService.HandleTrackIn(request);
            return Ok(new ApiResponse<object>(true, "上级完成"));
        } catch(Exception ex) {
            logger.LogError(ex, "处理上机请求时发生错误");
            return BadRequest(new ApiResponse<object>(false, "处理上机请求时发生错误"));
        }
    }

    /// <summary>
    /// 板件确认接口
    /// POST: /api/Equipment/EQP2DComfirm
    /// </summary>
    [HttpPost("EQP2DComfirm")]
    public IActionResult EQP2DComfirm([FromBody] EQP2DConfirmRequest request) {
        try {
            deviceService.HandleEQPConfirm(request);
            return Ok(new ApiResponse<object>(true, "2D confirm received"));
        } catch(Exception ex) {
            logger.LogError(ex, "处理板件确认请求时发生错误");
            return BadRequest(new ApiResponse<object>(false, "Error processing 2D confirm request"));
        }
    }

    /// <summary>
    /// 处理结束接口
    /// POST: /api/Equipment/ProcessEnd
    /// </summary>
    [HttpPost("ProcessEnd")]
    public IActionResult ProcessEnd([FromBody] ProcessEndRequest request) {
        try {
            deviceService.HandleProcessEnd(request);
            return Ok(new ApiResponse<object>(true, "Process end recorded"));
        } catch(Exception ex) {
            logger.LogError(ex, "处理流程结束请求时发生错误");
            return BadRequest(new ApiResponse<object>(false, "Error processing process end request"));
        }
    }
}

using ApiServer.Models;
using ApiServer.Services;
using ApiServer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers {
    [ApiController]
    [Route("api")]
    public class EquipmentController(IDeviceService deviceService, ILogger<EquipmentController> logger)
        :ControllerBase {
        [HttpPost("AliveCheck")]
        public IActionResult AliveCheck([FromBody] AliveCheckRequest request) {
            try {
                deviceService.HandleAliveCheck(request);
                return Ok(new ApiResponse<object>(true, "Alive check processed"));
            } catch(Exception ex) {
                logger.LogError(ex, "处理心跳请求时发生错误");
                return BadRequest(new ApiResponse<object>(false, "Error processing alive check"));
            }
        }

        [HttpPost("TrackInRequest")]
        public IActionResult TrackInRequest([FromBody] TrackInRequest request) {
            try {
                deviceService.HandleTrackIn(request);
                return Ok(new ApiResponse<object>(true, "TrackIn processed"));
            } catch(Exception ex) {
                logger.LogError(ex, "处理 TrackIn 请求时发生错误");
                return BadRequest(new ApiResponse<object>(false, "Error processing track in request"));
            }
        }

        [HttpPost("EQP2DComfirm")]
        public IActionResult EQP2DComfirm([FromBody] EQP2DConfirmRequest request) {
            try {
                deviceService.HandleEQPConfirm(request);
                return Ok(new ApiResponse<object>(true, "2D confirm received"));
            } catch(Exception ex) {
                logger.LogError(ex, "处理 EQP2DComfirm 请求时发生错误");
                return BadRequest(new ApiResponse<object>(false, "Error processing EQP2D confirm request"));
            }
        }

        [HttpPost("ProcessEnd")]
        public IActionResult ProcessEnd([FromBody] ProcessEndRequest request) {
            try {
                deviceService.HandleProcessEnd(request);
                return Ok(new ApiResponse<object>(true, "Process end recorded"));
            } catch(Exception ex) {
                logger.LogError(ex, "处理 ProcessEnd 请求时发生错误");
                return BadRequest(new ApiResponse<object>(false, "Error processing process end request"));
            }
        }
    }
}
using ApiServer.Models;
using ApiServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

[ApiController]
[Route("abc")]
public class AgvCallbackController(
    IAgvService agvService,
    IRcsService rcsService,
    ILogger<AgvCallbackController> logger) :ControllerBase {

    [HttpPost("agv/agvCallbackService/agvCallback")]
    public IActionResult Post([FromBody] AgvCallbackRequest request) {
        logger.LogInformation($"Received AGV callback: {request.ReqCode}");

        if(!ModelState.IsValid) {
            return BadRequest(new AgvCallbackResponse {
                Code = "400",
                Message = "Invalid request data",
                ReqCode = request?.ReqCode ?? "unknown"
            });
        }

        try {
            var response = agvService.HandleAgvCallback(request);
            return Ok(response);
        } catch(Exception ex) {
            logger.LogError(ex, "Error processing AGV callback");
            return StatusCode(500, new AgvCallbackResponse {
                Code = "500",
                Message = "Internal server error",
                ReqCode = request.ReqCode
            });
        }
    }

    [HttpPost("agv/create")]
    public async Task<IActionResult> CreateSchedulingTask([FromBody] GenAgvSchedulingTaskRequest com) {
        if(!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        logger.LogInformation($"收到AGV调度任务请求: {com.ReqCode} TaskType:{com.TaskType} ReqCode:{com.ReqCode}");

        // 服务端提供 ReqCode、ReqTime

        try {
            BaseRcsResponse rcs = await rcsService.GenerateAgvSchedulingTaskAsync(com);
            if(rcs != null && rcs.Code == "0") {
                return Ok(new {
                    message = "AGV 调度任务已成功通过 RCS 启动。",
                    sentRequestDetails = new {
                        originalReqCode = com.ReqCode,
                        taskType = com.TaskType
                    },
                    rcsResponse = new { // RCS系统的实际响应
                        echoedReqCode = rcs.ReqCode,
                        code = rcs.Code,
                        message = rcs.Message
                    }
                });
            } else {
                logger.LogError($"AGV调度任务请求失败: {rcs.ReqCode}, 错误代码: {rcs.Code}, 错误信息: {rcs.Message}");
                return StatusCode(500, new {
                    message = "无法使用 RCS 启动 AGV 调度任务。",
                    rcsErrorCode = "-1",
                });
            }
        } catch(Exception ex) {
            logger.LogError(ex, "AGV调度任务请求处理失败");
            return StatusCode(500, "发生内部服务器错误。");
        }
    }
}


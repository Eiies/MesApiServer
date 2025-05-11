using ApiServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

[ApiController]
[Route("api")]
public class AgvController :ControllerBase {
    [HttpPost("AskforFeed01")]
    public IActionResult AskforFeed01([FromBody] AskforFeed01Request request) {
        if(string.IsNullOrEmpty(request.AskCode)) {
            return BadRequest(new { resultCode = "0001", resultMsg = "请求低码为空" });
        }

        return Ok(new { resultCode = "0000", resultMsg = "OK" });
    }

    [HttpPost("AskforFeed02")]
    public IActionResult AskforFeed02([FromBody] AskforFeed02Request request) {
        if(string.IsNullOrEmpty(request.AskforType)) {
            return BadRequest(new { resultCode = "0001", resultMsg = "请求组合类型为空" });
        }

        return Ok(new {
            resultCode = "0000",
            PanelType = request.AskforType,
            resultMsg = "OK"
        });
    }

    [HttpPost("EnterSafe")]
    public IActionResult EnterSafe([FromBody] EnterSafeRequest request) {
        if(request.SafeState?.ToLower() != "safe") {
            return BadRequest(new { resultCode = "0001", resultMsg = "设备不安全" });
        }

        return Ok(new { resultCode = "0000", resultMsg = "OK" });
    }

    [HttpPost("BarcodeUpEvent")]
    public IActionResult BarcodeUpEvent([FromBody] BarcodeUpRequest request) {
        if(string.IsNullOrEmpty(request.PanelUp)) {
            return BadRequest(new { resultCode = "0001", resultMsg = "缺少板件信息" });
        }

        return Ok(new {
            resultCode = "0000",
            dataResult = $"{request.PanelUp};1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;X;A:7,8;B:7,8",
            resultMsg = "OK"
        });
    }
}


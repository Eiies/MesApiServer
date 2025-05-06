using ApiServer.Models;
using ApiServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

[ApiController]
[Route("api")]
public class RecordCsvController(IRecordService r, ILogger<RecordCsvController> logger) :ControllerBase {
    [HttpPost("csv")]
    public async Task<IActionResult> Post([FromBody] RecordCsvDto dto) {
        if(dto == null) {
            logger.LogWarning("无效数据: {@Dto}", dto);
            return BadRequest("Invalid data.");
        }
        await r.SaveRecordAsync(dto);
        return Ok(new { message = "保存成功" });
    }
}


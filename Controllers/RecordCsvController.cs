using ApiServer.Data;
using ApiServer.Models;
using ApiServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ApiServer.Controllers;

[ApiController]
[Route("api")]
public class RecordCsvController(AppDbContext context, IRecordService r, ILogger<RecordCsvController> logger) :ControllerBase {
    [HttpPost("csv")]
    public async Task<IActionResult> Post([FromBody] RecordCsvDto dto) {
        if(dto == null) {
            logger.LogWarning("无效数据: {@Dto}", dto);
            return BadRequest("Invalid data.");
        }
        await r.SaveRecordAsync(dto);
        return Ok(new { message = "保存成功" });
    }

    [HttpGet("csv/{qrCode}")]
    public async Task<ActionResult<RecordCsvDto>> GetRecord(string qrCode) {
        var e = await context.RecordCsvEntities
             .Include(r => r.Values)
             .FirstOrDefaultAsync(r => r.QRCode == qrCode);

        if(e == null)
            return NotFound();

        var dto = new RecordCsvDto {
            QRCode = e.QRCode,
            EngravingContent = e.EngravingContent,
            CategoryResult = e.CategoryResult,
            ANgPoints = e.ANgPoints,
            BNgPoints = e.BNgPoints,
            Group1 = e.Group1,
            Group2 = e.Group2,
            Group3 = e.Group3,
            Values = e.Values.ToDictionary(
                v => v.Index.ToString(),
                v => JsonSerializer.SerializeToElement(v.Value)
            )
        };

        return Ok(dto);
    }
}


using MesApiServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesApiServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(MesService mesService) : ControllerBase{
    private readonly MesService _mesService = mesService;

    [HttpGet("ping")]
    public IActionResult Ping(){
        return Ok("MES 服务已注入，接口正常！");
    }

    [HttpPost("ping")]
    public IActionResult Create(){
        return Ok("MES 服务已注入，接口正常！");
    }
}

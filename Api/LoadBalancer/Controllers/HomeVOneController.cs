using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("v1")]
    public IActionResult IndexV1()
    {
        return Ok("Hi from API V1");
    }

    [HttpGet("v2")]
    public IActionResult IndexV2()
    {
        return Ok("Hi from API V2");
    }

    [HttpGet("v3")]
    public IActionResult IndexV3()
    {
        return Ok("Hi from API V3");
    }
}
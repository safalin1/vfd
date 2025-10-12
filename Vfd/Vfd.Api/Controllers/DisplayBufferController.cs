using Microsoft.AspNetCore.Mvc;
using Vfd.Api.Services;

namespace Vfd.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DisplayBufferController : ControllerBase
{
    private readonly DisplayBuffer _displayBuffer;

    public DisplayBufferController(
        DisplayBuffer displayBuffer)
    {
        _displayBuffer = displayBuffer;
    }
    
    [HttpDelete]
    public IActionResult ClearBuffer()
    {
        _displayBuffer.TopLine = null;
        _displayBuffer.BottomLine = null;
        return Ok("Display buffers cleared");
    }

    [HttpPost]
    public IActionResult SetText([FromForm] string topLine, [FromForm] string bottomLine)
    {
        _displayBuffer.TopLine = topLine;
        _displayBuffer.BottomLine = bottomLine;
        
        return Ok("Display buffers set");
    }
}
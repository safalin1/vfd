using Microsoft.AspNetCore.Mvc;
using Vfd.Api.CommandSetTables;
using Vfd.Api.Services.Displays;

namespace Vfd.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DisplayController : ControllerBase
{
    private readonly IDisplayHardware _displayHardware;
    private readonly IVfdCommandSetTable _commandSetTable;

    public DisplayController(
        IDisplayHardware displayHardware,
        IVfdCommandSetTable commandSetTable)
    {
        _displayHardware = displayHardware;
        _commandSetTable = commandSetTable;
    }
    
    [HttpDelete("Blink")]
    public IActionResult ClearBlink()
    {
        Blink(0);
        return Ok("Blink turned off");
    }

    [HttpPost("Blink")]
    public IActionResult Blink([FromForm] int duration)
    {
        _displayHardware.Write(_commandSetTable.Blink(duration));
        return Ok($"Blink interval set to {duration}");
    }

    [HttpPost("SelfTest")]
    public IActionResult SelfTest()
    {
        _displayHardware.Write(_commandSetTable.SelfTest);
        return Ok("Self test initiated");
    }
}
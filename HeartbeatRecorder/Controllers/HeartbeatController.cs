using Microsoft.AspNetCore.Mvc;
using HeartbeatRecorder.Data;
using HeartbeatRecorder.Entities;

[ApiController]
[Route("api/[controller]")]
public class HeartbeatController : ControllerBase
{
    private readonly DataContext _context;

    public HeartbeatController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult PostHeartbeat(HeartbeatDto dto)
    {
        var heartbeat = new Heartbeat
        {
            SerialNumber = dto.SerialNumber,
            Value = dto.Value,
        };

        _context.Heartbeats.Add(heartbeat);
        _context.SaveChanges();

        return Ok("Heartbeat saved");
    }

    [HttpGet("{serialNumber}")]
    public IActionResult GetUserDataAndHeartbeats(string serialNumber)
    {
        var heartbeats = _context.Heartbeats
                           .Where(h => h.SerialNumber == serialNumber)
                           .Select(h => new { h.Value, h.Timestamp })
                           .ToList();

        if (!heartbeats.Any())
            return NotFound("No heartbeats found for this serial number");

        return Ok(heartbeats);
    }
}

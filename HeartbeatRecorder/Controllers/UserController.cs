using HeartbeatRecorder.Data;
using HeartbeatRecorder.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult GetUsersWithHeartbeats()
    {
        var usersWithHeartbeats = _context.Users
            .Select(u => new
            {
                u.Id,
                u.SerialNumber,
                u.Username,
                Heartbeats = _context.Heartbeats
                    .Where(h => h.SerialNumber == u.SerialNumber)
                    .Select(h => new { h.Value, h.Timestamp })
                    .ToList()
            })
            .ToList();

        return Ok(usersWithHeartbeats);
    }

    [HttpGet("{serialNumber}")]
    public IActionResult GetUser(string serialNumber)
    {
        var user = _context.Users.Where(u => u.SerialNumber == serialNumber)
            .Select(u => new
            {
                u.Id,
                u.SerialNumber,
                u.Username,
                Heartbeats = _context.Heartbeats
                    .Where(h => h.SerialNumber == u.SerialNumber)
                    .Select(h => new { h.Value, h.Timestamp })
                    .ToList()
            })
            .FirstOrDefault();

        if (user == null)
        {
            return BadRequest("No user found with this serial number.");
        }

        return Ok(user);
    }

    [HttpDelete]
    public IActionResult DeleteUser(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username && u.Password == dto.Password);
        if (user == null) return Unauthorized("Incorrect username or password!");

        _context.Users.Remove(user);
        _context.SaveChanges();

        return Ok(new { message = "User deleted successfully." });
    }
}

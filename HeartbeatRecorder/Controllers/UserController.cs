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
            .ToList();

        if (user.Count == 0)
        {
            return BadRequest("!کاربری با این شماره سریال وجود ندارد");
        }

        return Ok(user);
    }

    [HttpDelete]
    public IActionResult DeleteUser(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username && u.Password == dto.Password);
        if (user == null) return Unauthorized("نام کاربری یا رمز عبور اشتباه است!");

        _context.Users.Remove(user);
        _context.SaveChanges();

        return Ok(new { message = "کاربر با موفقیت حذف شد" });
    }
}

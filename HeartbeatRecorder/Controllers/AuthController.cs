using Microsoft.AspNetCore.Mvc;
using HeartbeatRecorder.Data;
using HeartbeatRecorder.Entities;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DataContext _context;

    public AuthController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("signup")]
    public IActionResult Signup(RegisterDto dto)
    {
        if (_context.Users.Any(u => u.SerialNumber == dto.SerialNumber))
            return BadRequest("This serial number is already registered.");

        var user = new User
        {
            Username = dto.Username,
            Password = dto.Password,
            SerialNumber = dto.SerialNumber
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new
        {
            message = "User registered successfully",
            user.SerialNumber
        });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username && u.Password == dto.Password);
        if (user == null) return Unauthorized("Invalid username or password!");

        return Ok(new { message = "Login successful", user.SerialNumber  });
    }
}

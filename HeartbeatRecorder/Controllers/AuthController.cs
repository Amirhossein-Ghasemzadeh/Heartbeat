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
            return BadRequest("این شماره سریال قبلا ثبت شده است!");

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
            message = "کاربر با موفقیت ثبت نام شد",
            user.SerialNumber
        });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username && u.Password == dto.Password);
        if (user == null) return Unauthorized("نام کاربری یا رمز عبور اشتباه اسنت!");

        return Ok(new { message = "Login successful", user.SerialNumber  });
    }
}

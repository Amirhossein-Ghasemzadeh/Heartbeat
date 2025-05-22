using Microsoft.AspNetCore.Mvc;
using HeartbeatRecorder.Data;
using HeartbeatRecorder.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public IActionResult GetUsers()
    {
        var user = _context.Users.ToList();
        return Ok(user);
    }

    [HttpDelete]
    public IActionResult DeleteUser(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username && u.Password == dto.Password);
        if (user == null) return Unauthorized("Invalid credentials");

        _context.Users.Remove(user);
        _context.SaveChanges();

        return Ok(new { message = "کاربر با موفقیت حذف شد" });
    }
}

using System.ComponentModel.DataAnnotations;

namespace HeartbeatRecorder.Entities
{
    public class RegisterDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string SerialNumber { get; set; }
    }
}

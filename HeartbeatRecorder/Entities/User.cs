using System.ComponentModel.DataAnnotations;

namespace HeartbeatRecorder.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string SerialNumber { get; set; }

        public List<Heartbeat> Heartbeats { get; set; } = new();
    }

}

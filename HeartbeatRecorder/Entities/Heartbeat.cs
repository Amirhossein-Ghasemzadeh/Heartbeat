using System.ComponentModel.DataAnnotations;

namespace HeartbeatRecorder.Entities
{
    public class Heartbeat
    {
        public int Id { get; set; }
        [Required]
        public required string SerialNumber { get; set; }
        [Required]
        public int Value { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}

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
        public DateTime Timestamp { get; set; } = GetIranTime();

        private static DateTime GetIranTime()
        {
            var utcNow = DateTime.UtcNow;
            var iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            var iranTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, iranTimeZone);
            return iranTime;
        }
    }

}

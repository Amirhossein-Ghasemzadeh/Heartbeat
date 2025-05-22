using Microsoft.EntityFrameworkCore;
using HeartbeatRecorder.Entities;


namespace HeartbeatRecorder.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Heartbeat> Heartbeats { get; set; }
    }
}
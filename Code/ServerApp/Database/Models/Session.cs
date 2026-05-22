namespace ServerApp.Models
{
    public class Session
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MachineId { get; set; }

        public string StartTime { get; set; }= string.Empty;

        public string EndTime { get; set; }= string.Empty;

        public bool IsActive { get; set; }
    }
}
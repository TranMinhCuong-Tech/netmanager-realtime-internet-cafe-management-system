namespace ServerApp.Models
{
    public class Machine
    {
        public int Id { get; set; }

        public string MachineName { get; set; } = string.Empty;

        public string IpAddress { get; set; }= string.Empty;

        public string Status { get; set; }= string.Empty;

        public string LastHeartbeat { get; set; }= string.Empty;
    }
}
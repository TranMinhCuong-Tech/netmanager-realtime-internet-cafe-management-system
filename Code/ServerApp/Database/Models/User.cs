namespace ServerApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }= string.Empty;

        public string Password { get; set; }= string.Empty;

        public string Role { get; set; }= string.Empty;

        public bool IsLocked { get; set; }

        public string LastLogin { get; set; }= string.Empty;
    }
}
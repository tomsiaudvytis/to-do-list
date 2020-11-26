using Common.Enums;

namespace Common.Models
{
    using System.Text.Json.Serialization;

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
    }
}
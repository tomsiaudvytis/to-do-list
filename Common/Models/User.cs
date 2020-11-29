using System.Text.Json.Serialization;

namespace Common.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
    }
}
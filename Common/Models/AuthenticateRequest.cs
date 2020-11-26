namespace Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AuthenticateRequest
    {
        [Required] 
        [EmailAddress(ErrorMessage = "Invalid Email address supplied")]
        public string Email { get; set; }

        [Required]
        [MinLength(12, ErrorMessage = "Password minimum lenght is 12 characters")]
        public string Password { get; set; }
    }
}
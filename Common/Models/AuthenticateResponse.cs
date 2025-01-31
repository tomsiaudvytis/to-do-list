﻿namespace Common.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Role = user.Role;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Token = token;
        }
    }
}
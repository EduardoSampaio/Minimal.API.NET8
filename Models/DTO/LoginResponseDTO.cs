﻿namespace Minimal.API.NET8.Models.DTO
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}

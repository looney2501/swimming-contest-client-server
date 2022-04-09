using System;

namespace Model.Domain.DTOs;

[Serializable]
public class AdminDTO
{
    public string Username { get; }
    public string Password { get; }

    public AdminDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
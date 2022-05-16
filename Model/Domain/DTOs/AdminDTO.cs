using System;

namespace Model.Domain.DTOs;

[Serializable]
public class AdminDTO
{
    public AdminDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }
    public string Password { get; }
}
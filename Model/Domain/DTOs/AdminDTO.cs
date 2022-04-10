using System;

namespace Model.Domain.DTOs;

[Serializable]
public class AdminDTO
{
    public String Username { get; }
    public String Password { get; }

    public AdminDTO(String username, String password)
    {
        Username = username;
        Password = password;
    }
}
using System;

namespace Model.Domain.Entities;

[Serializable]
public class Admin : Identifiable<int>
{
    public Admin(int id, string username, string password)
    {
        this.id = id;
        Username = username;
        Password = password;
    }

    public Admin(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public int id { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        var other = obj as Admin;
        if (other == null) return false;
        return Username == other.Username && Password == other.Password;
    }

    public bool Equals(Admin other)
    {
        if (other == null) return false;

        return Username == other.Username && Password == other.Password;
    }

    public override string ToString()
    {
        return "Admin{" +
               "ID=" + id +
               ", username='" + Username + '\'' +
               ", password='" + Password + '\'' +
               '}';
    }
}
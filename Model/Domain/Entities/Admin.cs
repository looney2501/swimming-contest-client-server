using System;

namespace Model.Domain.Entities;

[Serializable]
public class Admin: Identifiable<Int32>
{
    public int ID { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }

    public Admin(int id, string username, string password)
    {
        ID = id;
        this.Username = username;
        Password = password;
    }

    public Admin(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        Admin other = obj as Admin;
        if (other == null)
        {
            return false;
        }
        return Username == other.Username && Password == other.Password;
    }

    public bool Equals(Admin other)
    {
        if (other == null)
        {
            return false;
        }
        
        return Username == other.Username && Password == other.Password;
    }

    public override string ToString()
    {
        return "Admin{" +
               "ID=" + ID +
               ", username='" + Username + '\'' +
               ", password='" + Password + '\'' +
               '}';
    }
}
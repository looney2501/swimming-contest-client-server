using System;

namespace MPP_lab_project.Domain;

[Serializable()]
public class Admin: Identifiable<Int32>
{
    public Int32 ID { get; set; }
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

    protected bool Equals(Admin other)
    {
        return Username == other.Username && Password == other.Password;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((Username != null ? Username.GetHashCode() : 0) * 397) ^ (Password != null ? Password.GetHashCode() : 0);
        }
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
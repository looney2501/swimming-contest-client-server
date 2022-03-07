using System;

namespace MPP_lab_project.Domain;

[Serializable()]
public class Admin: Identifiable<Int32>
{
    public Int32 ID { get; set; }
    public String username { get; set; }
    public String Password { get; set; }

    public Admin(int id, string username, string password)
    {
        ID = id;
        this.username = username;
        Password = password;
    }

    public Admin(string username, string password)
    {
        this.username = username;
        Password = password;
    }
}
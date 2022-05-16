using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Domain.Entities;

[Serializable]
public class Swimmer : Identifiable<int>
{
    public Swimmer(int id, string firstName, string lastName, int age)
    {
        this.id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public Swimmer(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    [Key] public int id { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        var other = obj as Swimmer;
        if (other == null) return false;
        return other.FirstName == FirstName && other.LastName == LastName && other.Age == Age;
    }

    public bool Equals(Swimmer other)
    {
        if (other == null) return false;
        return other.FirstName == FirstName && other.LastName == LastName && other.Age == Age;
    }

    public override string ToString()
    {
        return "Swimmer{" +
               "ID=" + id +
               ", firstName='" + FirstName + '\'' +
               ", lastName='" + LastName + '\'' +
               ", age=" + Age +
               '}';
    }
}
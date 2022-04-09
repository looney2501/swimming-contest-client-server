using System;

namespace Model.Domain.Entities;

[Serializable]
public class Swimmer: Identifiable<Int32>
{
    public int ID { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public Int32 Age { get; set; }

    public Swimmer(int id, string firstName, string lastName, int age)
    {
        ID = id;
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

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        Swimmer other = obj as Swimmer;
        if (other == null)
        {
            return false;
        }
        return other.FirstName == FirstName && other.LastName == LastName && other.Age == Age;
    }

    public bool Equals(Swimmer other)
    {
        if (other == null)
        {
            return false;
        }
        return other.FirstName == FirstName && other.LastName == LastName && other.Age == Age;
    }

    public override string ToString()
    {
        return "Swimmer{" +
               "ID=" + ID +
               ", firstName='" + FirstName + '\'' +
               ", lastName='" + LastName + '\'' +
               ", age=" + Age +
               '}';
    }
}
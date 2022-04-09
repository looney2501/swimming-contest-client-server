using System;

namespace Model.Services;

public class ServicesException : Exception
{
    public ServicesException(string message) : base(message)
    {
    }
}
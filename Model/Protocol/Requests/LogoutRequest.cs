using System;

namespace Model.Protocol.Requests;

[Serializable]
public class LogoutRequest : IRequest
{
    public LogoutRequest(string username)
    {
        Username = username;
    }

    public string Username { get; }
}
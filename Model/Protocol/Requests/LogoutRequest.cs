using System;

namespace Model.Protocol.Requests;

[Serializable]
public class LogoutRequest : IRequest
{
    public string Username { get; }

    public LogoutRequest(string username)
    {
        Username = username;
    }
}
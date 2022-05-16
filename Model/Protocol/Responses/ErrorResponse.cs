using System;

namespace Model.Protocol.Responses;

[Serializable]
public class ErrorResponse : IResponse
{
    public ErrorResponse(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }
}
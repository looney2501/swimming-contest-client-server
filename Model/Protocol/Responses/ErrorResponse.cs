using System;

namespace Model.Protocol.Responses;

[Serializable]
public class ErrorResponse: IResponse
{
    public string ErrorMessage { get; }

    public ErrorResponse(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
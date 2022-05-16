using System;
using Model.Domain.DTOs;

namespace Model.Protocol.Requests;

[Serializable]
public class AddSwimmerRequest : IRequest
{
    public AddSwimmerRequest(SwimmerDTO swimmerDto)
    {
        SwimmerDTO = swimmerDto;
    }

    public SwimmerDTO SwimmerDTO { get; }
}
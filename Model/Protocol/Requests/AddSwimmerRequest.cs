using System;
using Model.Domain.DTOs;

namespace Model.Protocol.Requests;

[Serializable]
public class AddSwimmerRequest : IRequest
{
    public SwimmerDTO SwimmerDTO { get; }

    public AddSwimmerRequest(SwimmerDTO swimmerDto)
    {
        SwimmerDTO = swimmerDto;
    }
}
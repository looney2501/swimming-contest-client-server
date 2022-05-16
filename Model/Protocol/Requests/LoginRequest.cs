using System;
using Model.Domain.DTOs;

namespace Model.Protocol.Requests;

[Serializable]
public class LoginRequest : IRequest
{
    public LoginRequest(AdminDTO adminDto)
    {
        AdminDTO = adminDto;
    }

    public AdminDTO AdminDTO { get; }
}
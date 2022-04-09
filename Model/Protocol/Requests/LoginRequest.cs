using System;
using Model.Domain.DTOs;

namespace Model.Protocol.Requests;

[Serializable]
public class LoginRequest: IRequest 
{
    public AdminDTO AdminDTO { get; }

    public LoginRequest(AdminDTO adminDto)
    {
        AdminDTO = adminDto;
    }
}
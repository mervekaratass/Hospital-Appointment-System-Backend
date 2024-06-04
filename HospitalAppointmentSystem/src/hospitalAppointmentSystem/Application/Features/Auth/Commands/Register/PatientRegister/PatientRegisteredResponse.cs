using NArchitecture.Core.Security.JWT;
using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register.PatientRegister;
public class PatientRegisteredResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public Domain.Entities.RefreshToken RefreshToken { get; set; }

    public PatientRegisteredResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public PatientRegisteredResponse(AccessToken accessToken, Domain.Entities.RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.ChangePassword;
public class ChangePasswordResponse:IResponse
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }

    public ChangePasswordResponse()
    {
        Password = string.Empty;
        NewPassword = string.Empty;

    }
    public ChangePasswordResponse(Guid Id,string password,string newPassword)
    {
        Id = Id;
        Password = password;
        NewPassword = newPassword;
    }



}

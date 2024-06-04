using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register.UserRegister;
public class UserForRegisterDto : NArchitecture.Core.Application.Dtos.UserForRegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Commands.Register.UserRegister;

namespace Application.Features.Auth.Commands.Register.DoctorRegister;
public class DoctorForRegisterDto : UserForRegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}

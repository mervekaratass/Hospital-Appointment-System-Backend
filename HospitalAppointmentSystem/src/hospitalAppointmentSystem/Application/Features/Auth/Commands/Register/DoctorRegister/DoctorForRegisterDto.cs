using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Commands.Register.UserRegister;

namespace Application.Features.Auth.Commands.Register.DoctorRegister;
public class DoctorForRegisterDto : UserForRegisterDto
{
    public  string Title { get; set; }
    public  string SchoolName { get; set; }
    public  int BranchID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
 
}

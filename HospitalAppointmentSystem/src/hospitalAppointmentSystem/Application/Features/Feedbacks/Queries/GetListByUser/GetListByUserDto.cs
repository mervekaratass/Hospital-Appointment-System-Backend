using NArchitecture.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedbacks.Queries.GetListByUser;
public class GetListByUserDto : IDto
{
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }    
    public bool IsApproved { get; set; }

}

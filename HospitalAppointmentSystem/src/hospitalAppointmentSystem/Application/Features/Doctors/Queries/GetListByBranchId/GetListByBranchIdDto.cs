using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.GetListByBranchId;
public class GetListByBranchIdDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int BranchID { get; set; }
    public string BranchName { get; set; }
  
  
}

using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Feedbacks.Queries.GetList;

public class GetListFeedbackListItemDto : IDto
{
    public int Id { get; set; }    
    public Guid UserID { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsApproved { get; set; }

}
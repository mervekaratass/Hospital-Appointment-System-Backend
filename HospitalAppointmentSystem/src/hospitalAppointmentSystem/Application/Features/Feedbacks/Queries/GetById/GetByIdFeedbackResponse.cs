using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Feedbacks.Queries.GetById;

public class GetByIdFeedbackResponse : IDto
{
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string Text { get; set; }
    public bool IsApproved { get; set; }
}
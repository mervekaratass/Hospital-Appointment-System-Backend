using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Feedbacks.Queries.GetList;

public class GetListFeedbackListItemDto : IDto
{
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public string Text { get; set; }
}
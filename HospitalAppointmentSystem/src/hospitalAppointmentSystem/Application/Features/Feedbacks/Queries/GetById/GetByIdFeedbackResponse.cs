using NArchitecture.Core.Application.Responses;

namespace Application.Features.Feedbacks.Queries.GetById;

public class GetByIdFeedbackResponse : IResponse
{
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public string Text { get; set; }
}
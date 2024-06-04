using NArchitecture.Core.Application.Responses;

namespace Application.Features.Feedbacks.Commands.Update;

public class UpdatedFeedbackResponse : IResponse
{
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public string Text { get; set; }
}
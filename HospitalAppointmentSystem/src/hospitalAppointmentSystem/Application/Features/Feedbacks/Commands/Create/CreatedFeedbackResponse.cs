using NArchitecture.Core.Application.Responses;

namespace Application.Features.Feedbacks.Commands.Create;

public class CreatedFeedbackResponse : IResponse
{
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public string Text { get; set; }
    public bool IsApproved { get; set; }
}
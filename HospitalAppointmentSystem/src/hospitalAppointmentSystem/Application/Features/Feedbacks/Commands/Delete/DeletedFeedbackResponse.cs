using NArchitecture.Core.Application.Responses;

namespace Application.Features.Feedbacks.Commands.Delete;

public class DeletedFeedbackResponse : IResponse
{
    public int Id { get; set; }
}
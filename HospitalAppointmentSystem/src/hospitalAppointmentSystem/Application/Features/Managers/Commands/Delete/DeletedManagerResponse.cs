using NArchitecture.Core.Application.Responses;

namespace Application.Features.Managers.Commands.Delete;

public class DeletedManagerResponse : IResponse
{
    public Guid Id { get; set; }
}
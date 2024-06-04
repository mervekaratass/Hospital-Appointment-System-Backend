using NArchitecture.Core.Application.Responses;

namespace Application.Features.Patients.Commands.Delete;

public class DeletedPatientResponse : IResponse
{
    public Guid Id { get; set; }
}
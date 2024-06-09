using NArchitecture.Core.Application.Responses;

namespace Application.Features.Doctors.Commands.Delete;

public class DeletedDoctorResponse : IResponse
{
    public Guid Id { get; set; }
  //  public DateTime DeletedDate { get; set; }

}
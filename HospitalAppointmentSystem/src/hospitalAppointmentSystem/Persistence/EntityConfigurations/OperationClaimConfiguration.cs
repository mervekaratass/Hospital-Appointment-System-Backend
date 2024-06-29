using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Appointments.Constants;
using Application.Features.Branches.Constants;
using Application.Features.Doctors.Constants;
using Application.Features.DoctorSchedules.Constants;
using Application.Features.Managers.Constants;
using Application.Features.Notifications.Constants;
using Application.Features.Patients.Constants;
using Application.Features.Reports.Constants;
using Application.Features.Feedbacks.Commands.Constants;


namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion


        
        
        #region Appointments CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AppointmentsOperationClaims.Admin },
                new() { Id = ++lastId, Name = AppointmentsOperationClaims.Read },
                new() { Id = ++lastId, Name = AppointmentsOperationClaims.Write },
                new() { Id = ++lastId, Name = AppointmentsOperationClaims.Create },
                new() { Id = ++lastId, Name = AppointmentsOperationClaims.Update },
                new() { Id = ++lastId, Name = AppointmentsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Branches CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BranchesOperationClaims.Admin },
                new() { Id = ++lastId, Name = BranchesOperationClaims.Read },
                new() { Id = ++lastId, Name = BranchesOperationClaims.Write },
                new() { Id = ++lastId, Name = BranchesOperationClaims.Create },
                new() { Id = ++lastId, Name = BranchesOperationClaims.Update },
                new() { Id = ++lastId, Name = BranchesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Doctors CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = DoctorsOperationClaims.Admin },
                new() { Id = ++lastId, Name = DoctorsOperationClaims.Read },
                new() { Id = ++lastId, Name = DoctorsOperationClaims.Write },
                new() { Id = ++lastId, Name = DoctorsOperationClaims.Create },
                new() { Id = ++lastId, Name = DoctorsOperationClaims.Update },
                new() { Id = ++lastId, Name = DoctorsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region DoctorSchedules CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = DoctorSchedulesOperationClaims.Admin },
                new() { Id = ++lastId, Name = DoctorSchedulesOperationClaims.Read },
                new() { Id = ++lastId, Name = DoctorSchedulesOperationClaims.Write },
                new() { Id = ++lastId, Name = DoctorSchedulesOperationClaims.Create },
                new() { Id = ++lastId, Name = DoctorSchedulesOperationClaims.Update },
                new() { Id = ++lastId, Name = DoctorSchedulesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Feedbacks CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = FeedbacksOperationClaims.Admin },
                new() { Id = ++lastId, Name = FeedbacksOperationClaims.Read },
                new() { Id = ++lastId, Name = FeedbacksOperationClaims.Write },
                new() { Id = ++lastId, Name = FeedbacksOperationClaims.Create },
                new() { Id = ++lastId, Name = FeedbacksOperationClaims.Update },
                new() { Id = ++lastId, Name = FeedbacksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Managers CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ManagersOperationClaims.Admin },
                new() { Id = ++lastId, Name = ManagersOperationClaims.Read },
                new() { Id = ++lastId, Name = ManagersOperationClaims.Write },
                new() { Id = ++lastId, Name = ManagersOperationClaims.Create },
                new() { Id = ++lastId, Name = ManagersOperationClaims.Update },
                new() { Id = ++lastId, Name = ManagersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Notifications CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = NotificationsOperationClaims.Admin },
                new() { Id = ++lastId, Name = NotificationsOperationClaims.Read },
                new() { Id = ++lastId, Name = NotificationsOperationClaims.Write },
                new() { Id = ++lastId, Name = NotificationsOperationClaims.Create },
                new() { Id = ++lastId, Name = NotificationsOperationClaims.Update },
                new() { Id = ++lastId, Name = NotificationsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Patients CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PatientsOperationClaims.Admin },
                new() { Id = ++lastId, Name = PatientsOperationClaims.Read },
                new() { Id = ++lastId, Name = PatientsOperationClaims.Write },
                new() { Id = ++lastId, Name = PatientsOperationClaims.Create },
                new() { Id = ++lastId, Name = PatientsOperationClaims.Update },
                new() { Id = ++lastId, Name = PatientsOperationClaims.Delete },
            ]
        );
        #endregion
        

        #region Reports CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ReportsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Read },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Write },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Create },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Update },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}

using AutoMapper;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Application.Services.Repositories;
using Application.Services;
using Domain.Entities;
using System.Threading.Tasks;
using Application.Features.Branches.Commands.Create;
using Application.Features.Branches.Constants;
using Application.Services.Doctors;
using NArchitecture.Core.Application.Rules;

namespace Application.Features.Branches.Rules
{
    public class BranchBusinessRules : BaseBusinessRules
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IDoctorService _doctorService; 
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public BranchBusinessRules(IBranchRepository branchRepository, IDoctorService doctorService, ILocalizationService localizationService, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _doctorService = doctorService;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        private async Task throwBusinessException(string messageKey)
        {
            string message = await _localizationService.GetLocalizedAsync(messageKey, BranchesBusinessMessages.SectionName);
            throw new BusinessException(message);
        }

        public async Task BranchShouldExistWhenSelected(Branch? branch)
        {
            if (branch == null)
                await throwBusinessException(BranchesBusinessMessages.BranchNotExists);
        }

        public async Task BranchIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
        {
            Branch? branch = await _branchRepository.GetAsync(
                predicate: b => b.Id == id,
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await BranchShouldExistWhenSelected(branch);
        }

        public async Task<Branch?> CheckIfBranchNameExists(string branchName)
        {
            Branch? existingBranch = await _branchRepository.GetAsync(b => b.Name == branchName);
            return existingBranch;
        }

        public async Task CheckIfBranchNameExistsAndNotDeleted(string branchName)
        {
            Branch? existingBranch = await CheckIfBranchNameExists(branchName);
            if (existingBranch != null && existingBranch.DeletedDate == null)
            {
                await throwBusinessException(BranchesBusinessMessages.BranchAlreadyExists);
            }
        }

        public async Task<CreatedBranchResponse?> CheckIfBranchNameExistsAndUndelete(string branchName)
        {
            Branch? existingBranch = await _branchRepository.GetAsync(b => b.Name == branchName && b.DeletedDate != null);

            if (existingBranch != null)
            {
                existingBranch.DeletedDate = null;
                await _branchRepository.UpdateAsync(existingBranch);
                CreatedBranchResponse response = _mapper.Map<CreatedBranchResponse>(existingBranch);
                return response;
            }

            return null;
        }

        public async Task CheckIfDoctorsExistInBranch(int branchId)
        {
            bool anyDoctorsExist = await _doctorService.AnyDoctorsInBranch(branchId);
            if (anyDoctorsExist)
            {
                await throwBusinessException(BranchesBusinessMessages.CannotDeleteBranchWithDoctors);
            }
        }

        public async Task<CreatedBranchResponse?> CheckIfBranchNameExistsAndHandle(string branchName)
        {
            // Check if the branch name exists but deleted, if so undelete
            CreatedBranchResponse? existingBranchResponse = await CheckIfBranchNameExistsAndUndelete(branchName);

            if (existingBranchResponse != null)
            {
                return existingBranchResponse;
            }

            await CheckIfBranchNameExistsAndNotDeleted(branchName);
            return null;
        }


    }
}

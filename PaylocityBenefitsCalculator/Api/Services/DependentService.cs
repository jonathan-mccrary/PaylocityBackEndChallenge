using System;
using Api.DataAccess;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Contracts;
using AutoMapper;

namespace Api.Services
{
	public class DependentService : IDependentService
    {
        private readonly IBenefitsRepository _benefitsRepository;
        private readonly IMapper _mapper;

        public DependentService(IBenefitsRepository benefitsRepository,
            IMapper mapper)
        {
            _benefitsRepository = benefitsRepository;
            _mapper = mapper;
        }

        public int AddDependent(Dependent dependent)
        {
            return _benefitsRepository.AddDependent(dependent);
        }

        public async Task<int> AddDependentAsync(Dependent dependent)
        {
            return await _benefitsRepository.AddDependentAsync(dependent);
        }

        public GetDependentDto? GetDependentById(int id)
        {
            var dependent = _benefitsRepository.GetDependentById(id);
            return _mapper.Map<GetDependentDto>(dependent);
        }

        public async Task<GetDependentDto?> GetDependentByIdAsync(int id)
        {
            var dependent = await _benefitsRepository.GetDependentByIdAsync(id);
            return _mapper.Map<GetDependentDto>(dependent);
        }

        public List<GetDependentDto> GetDependents()
        {
            var dependents = _benefitsRepository.GetDependents();

            return _mapper.Map<List<GetDependentDto>>(dependents);
        }

        public async Task<List<GetDependentDto>> GetDependentsAsync()
        {
            var dependents = await _benefitsRepository.GetDependentsAsync();

            return _mapper.Map<List<GetDependentDto>>(dependents);
        }
    }
}


using System;
using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Services.Contracts
{
	public interface IDependentService
	{
        int? AddDependent(Dependent dependent);
        Task<int?> AddDependentAsync(Dependent dependent);
        DependentDto? GetDependentById(int id);
        Task<DependentDto?> GetDependentByIdAsync(int id);
        List<DependentDto> GetDependents();
        Task<List<DependentDto>> GetDependentsAsync();
    }
}


using System;
using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Services.Contracts
{
	public interface IDependentService
	{
        int AddDependent(Dependent dependent);
        Task<int> AddDependentAsync(Dependent dependent);
        GetDependentDto? GetDependentById(int id);
        Task<GetDependentDto?> GetDependentByIdAsync(int id);
        List<GetDependentDto> GetDependents();
        Task<List<GetDependentDto>> GetDependentsAsync();
    }
}


using ContractManager.Domain.Entities;

namespace ContractManager.Application.Interfaces
{
    public interface IContractRepository
    {
        Task<List<Contract>> GetAllAsync();
        Task AddAsync(Contract contract);
    }
}

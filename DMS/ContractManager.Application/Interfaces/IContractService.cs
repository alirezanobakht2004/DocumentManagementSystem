using ContractManager.Domain.Entities; 

namespace ContractManager.Application.Interfaces
{
    public interface IContractService
    {
        Task<List<Contract>> GetAllAsync();
        Task AddAsync(Contract contract);
    }
}

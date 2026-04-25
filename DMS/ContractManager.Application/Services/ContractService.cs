using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;

namespace ContractManager.Application.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _repository;

        public ContractService(IContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Contract>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Contract contract)
        {
            // اینجا می‌توانید منطق تجاری (Business Logic) یا اعتبارسنجی‌ها را قبل از ذخیره اضافه کنید
            await _repository.AddAsync(contract);
        }
    }
}

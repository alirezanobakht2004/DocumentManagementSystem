using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDbContext _context;

        public ContractRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> GetAllAsync()
        {
            return await _context.Contracts.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
        }
    }
}

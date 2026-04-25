using System;

namespace ContractManager.Domain.Entities
{
    public class ContractFinancialStatement : BaseEntity
    {
        public int ContractId { get; set; }
        public int StatementNumber { get; set; } // شماره صورت وضعیت
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

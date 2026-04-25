using System;

namespace ContractManager.Domain.Entities
{
    public class ContractTest : BaseEntity
    {
        public int ContractId { get; set; }
        public string TestName { get; set; } = string.Empty;
        public DateTime TestDate { get; set; }
        public string ResultSummary { get; set; } = string.Empty;
    }
}

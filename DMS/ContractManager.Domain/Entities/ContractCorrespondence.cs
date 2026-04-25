using System;

namespace ContractManager.Domain.Entities
{
    public class ContractCorrespondence : BaseEntity
    {
        public int ContractId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string LetterNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

using System;

namespace ContractManager.Domain.Entities
{
    public class ContractDutyRelease : BaseEntity
    {
        public int ContractId { get; set; }
        public string ReleaseType { get; set; } = string.Empty; // تعهدات 3400، 5 درصد اول 3500 و ...
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

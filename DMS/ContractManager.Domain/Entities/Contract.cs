using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public string ContractTitle { get; set; } = string.Empty;
        public string ContractNumber { get; set; } = string.Empty;
        public int ContractYear { get; set; }
        public string ContractorName { get; set; } = string.Empty;

        // مشخصات قرارداد
        public decimal InitialAmount { get; set; } // مبلغ پیمان
        public string? ContractCoefficients { get; set; } // ضرایب
        public string? BaseContractReference { get; set; } // قرارداد اولیه
        public string? BaseListReference { get; set; } // فهرست مبنای پیمان

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ContractStatus Status { get; set; } = ContractStatus.Active;

        // Navigation Properties
        public ICollection<ContractMeeting> Meetings { get; set; } = new List<ContractMeeting>();
        public ICollection<ContractCorrespondence> Correspondences { get; set; } = new List<ContractCorrespondence>();
        public ICollection<ContractTest> Tests { get; set; } = new List<ContractTest>();
        public ICollection<ContractFinancialStatement> FinancialStatements { get; set; } = new List<ContractFinancialStatement>();
        public ICollection<ContractDelivery> Deliveries { get; set; } = new List<ContractDelivery>();
        public ICollection<ContractDutyRelease> DutyReleases { get; set; } = new List<ContractDutyRelease>();
    }
}

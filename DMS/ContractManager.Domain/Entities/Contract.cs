namespace ContractManager.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ContractNumber { get; set; } = string.Empty;
        public string ContractorName { get; set; } = string.Empty;

        public decimal ContractAmount { get; set; }
        public string Factors { get; set; } = string.Empty; // ضرایب
        public bool IsInitialContract { get; set; }
        public string BaseList { get; set; } = string.Empty; // فهرست مبنای پیمان
    }
}

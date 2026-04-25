using System;
using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class ContractDelivery : BaseEntity
    {
        public int ContractId { get; set; }
        public DeliveryType Type { get; set; } // موقت یا قطعی
        public DateTime DeliveryDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

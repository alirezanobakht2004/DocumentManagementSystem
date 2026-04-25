using System;
using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class ContractMeeting : BaseEntity
    {
        public int ContractId { get; set; }
        public Contract? Contract { get; set; }

        public MeetingType MeetingType { get; set; }
        public DateTime MeetingDate { get; set; }
        public string? Description { get; set; } // متن یا خلاصه نامه
        public decimal? Amount { get; set; } // برای صورتجلسات انجام
    }
}

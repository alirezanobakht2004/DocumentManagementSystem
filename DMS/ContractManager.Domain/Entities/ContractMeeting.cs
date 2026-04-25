using System;
using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class ContractMeeting : BaseEntity
    {
        public int ContractId { get; set; }
        public MeetingType Type { get; set; } // تحویل زمین، شروع به کار و ...
        public string LetterNumber { get; set; } = string.Empty;
        public DateTime MeetingDate { get; set; }
        public decimal? Amount { get; set; } // مبلغ ریالی (اختیاری)
        public string Description { get; set; } = string.Empty;
    }
}

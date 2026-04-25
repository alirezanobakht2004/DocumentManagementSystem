using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class Attachment : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty; // مثلا .pdf یا .jpg
        public string RelativePath { get; set; } = string.Empty; // مسیر فایل در هارد دیسک
        public long FileSize { get; set; }

        // ارتباط چندریختی (Polymorphic)
        public EntityType RelatedEntityType { get; set; } // مثلا ContractMeeting یا ContractTest
        public int RelatedEntityId { get; set; }
    }
}

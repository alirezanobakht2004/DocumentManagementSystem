using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class Attachment : BaseEntity
    {
        public int ContractId { get; set; }
        public Contract? Contract { get; set; }

        // Polymorphic Relations
        public EntityType RelatedEntityType { get; set; }
        public int RelatedEntityId { get; set; }

        public string FilePath { get; set; } = string.Empty; // مسیر روی دیسک
        public string FileType { get; set; } = string.Empty; // pdf, jpg, ...
        public long FileSize { get; set; }
        public bool HasOCR { get; set; }

        public OCRResult? OCRResult { get; set; }
    }
}

namespace ContractManager.Domain.Entities
{
    public class OCRResult : BaseEntity
    {
        public int AttachmentId { get; set; }
        public Attachment? Attachment { get; set; }

        public string ExtractedText { get; set; } = string.Empty;
        public string Language { get; set; } = "fas+eng"; // پیش‌فرض فارسی و انگلیسی
        public double Confidence { get; set; }
    }
}

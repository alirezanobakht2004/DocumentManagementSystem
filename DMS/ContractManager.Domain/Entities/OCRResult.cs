namespace ContractManager.Domain.Entities
{
    public class OCRResult : BaseEntity
    {
        public int AttachmentId { get; set; } // کلید خارجی به جدول Attachment
        public string ExtractedText { get; set; } = string.Empty; // متن استخراج شده
        public string Language { get; set; } = "fas+eng"; // زبان OCR
        public double Confidence { get; set; } // درصد اطمینان موتور OCR
    }
}

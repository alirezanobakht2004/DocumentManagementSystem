namespace ContractManager.Domain.Enums
{
    public enum EntityType
    {
        Contract = 1,                   // قرارداد اصلی
        ContractMeeting = 2,            // صورتجلسه
        ContractCorrespondence = 3,     // مکاتبه
        ContractTest = 4,               // آزمایش
        ContractFinancialStatement = 5, // صورت وضعیت مالی
        ContractDelivery = 6,           // تحویل
        ContractDutyRelease = 7         // آزادسازی تعهدات
    }
}

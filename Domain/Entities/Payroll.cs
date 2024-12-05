namespace Domain.Entities
{
    public class Payroll : SoftDeletableEntity
    {
        public DateTimeOffset PeriodStart { get; set; }
        public DateTimeOffset PeriodEnd { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal TotalPay { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public int GuardId { get; set; }
        public Guard Guard { get; set; }
    }
}

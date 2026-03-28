namespace Student.Api.DTOs
{
    public class StudentFeeSummaryDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string ParentName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public decimal TotalPendingAmount { get; set; }
        public List<PendingMonthDto> PendingMonths { get; set; } = new();
    }

    public class PendingMonthDto
    {
        public string Month { get; set; } = string.Empty;
        public int MonthNumber { get; set; }
        public decimal Amount { get; set; }
        public string DueDate { get; set; } = string.Empty;
        public int DaysOverdue { get; set; }
    }

    public class StudentFeePaymentRequestDto
    {
        public int StudentId { get; set; }
        public List<int> SelectedMonths { get; set; } = new();
        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class FeePaymentResultDto
    {
        public int StudentId { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string ReceiptNumber { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public string PaidDate { get; set; } = string.Empty;
        public List<int> PaidMonths { get; set; } = new();
    }
}

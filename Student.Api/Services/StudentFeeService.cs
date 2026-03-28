using Student.Api.DTOs;

namespace Student.Api.Services
{
    public class StudentFeeService : IStudentFeeService
    {
        private readonly List<StudentFeeSummaryDto> _studentFees = new()
        {
            new StudentFeeSummaryDto
            {
                StudentId = 1001,
                StudentName = "Aarav Patel",
                Class = "10th Grade",
                ParentName = "Rajesh Patel",
                ContactNumber = "+91 98989 89898",
                PendingMonths = new List<PendingMonthDto>
                {
                    new PendingMonthDto
                    {
                        Month = "March",
                        MonthNumber = 3,
                        Amount = 5000M,
                        DueDate = "2026-03-10",
                        DaysOverdue = 12,
                    },
                    new PendingMonthDto
                    {
                        Month = "April",
                        MonthNumber = 4,
                        Amount = 5000M,
                        DueDate = "2026-04-10",
                        DaysOverdue = 8,
                    },
                    new PendingMonthDto
                    {
                        Month = "May",
                        MonthNumber = 5,
                        Amount = 5000M,
                        DueDate = "2026-05-10",
                        DaysOverdue = 0,
                    }
                }
            }
        };

        public StudentFeeService()
        {
            foreach (var student in _studentFees)
            {
                student.TotalPendingAmount = student.PendingMonths.Sum(pm => pm.Amount);
            }
        }

        public Task<StudentFeeSummaryDto?> GetStudentFeesAsync(int studentId)
        {
            var student = _studentFees.FirstOrDefault(x => x.StudentId == studentId);
            return Task.FromResult(student);
        }

        public Task<FeePaymentResultDto?> ProcessPaymentAsync(StudentFeePaymentRequestDto request)
        {
            var student = _studentFees.FirstOrDefault(x => x.StudentId == request.StudentId);
            if (student is null)
            {
                return Task.FromResult<FeePaymentResultDto?>(null);
            }

            var selectedPending = student.PendingMonths
                .Where(pm => request.SelectedMonths.Contains(pm.MonthNumber))
                .ToList();

            if (!selectedPending.Any())
            {
                return Task.FromResult<FeePaymentResultDto?>(null);
            }

            var amountPaid = selectedPending.Sum(pm => pm.Amount);
            student.PendingMonths.RemoveAll(pm => request.SelectedMonths.Contains(pm.MonthNumber));
            student.TotalPendingAmount = student.PendingMonths.Sum(pm => pm.Amount);

            var paymentResult = new FeePaymentResultDto
            {
                StudentId = student.StudentId,
                TransactionId = $"TXN-{student.StudentId}-{DateTime.UtcNow:yyyyMMddHHmmss}",
                ReceiptNumber = $"RCP-{student.StudentId}-{DateTime.UtcNow:yyyyMMddHHmmss}",
                PaymentMethod = request.PaymentMethod,
                AmountPaid = amountPaid,
                PaidDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                PaidMonths = selectedPending.Select(pm => pm.MonthNumber).ToList()
            };

            return Task.FromResult<FeePaymentResultDto?>(paymentResult);
        }
    }
}

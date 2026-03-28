using Student.Api.DTOs;

namespace Student.Api.Services
{
    public interface IStudentFeeService
    {
        Task<StudentFeeSummaryDto?> GetStudentFeesAsync(int studentId);
        Task<FeePaymentResultDto?> ProcessPaymentAsync(StudentFeePaymentRequestDto request);
    }
}

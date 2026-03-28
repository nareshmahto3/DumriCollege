using MediatR;
using Student.Api.DTOs;

namespace Student.Api.CQRS.Commands
{
    public class PayStudentFeesCommand : IRequest<ResponseDto>
    {
        public StudentFeePaymentRequestDto PaymentRequest { get; }

        public PayStudentFeesCommand(StudentFeePaymentRequestDto paymentRequest)
        {
            PaymentRequest = paymentRequest;
        }
    }
}

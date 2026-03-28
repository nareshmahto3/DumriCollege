using MediatR;
using Student.Api.DTOs;
using Student.Api.Services;

namespace Student.Api.CQRS.Commands
{
    public class PayStudentFeesCommandHandler : IRequestHandler<PayStudentFeesCommand, ResponseDto>
    {
        private readonly IStudentFeeService _studentFeeService;

        public PayStudentFeesCommandHandler(IStudentFeeService studentFeeService)
        {
            _studentFeeService = studentFeeService;
        }

        public async Task<ResponseDto> Handle(PayStudentFeesCommand request, CancellationToken cancellationToken)
        {
            var paymentResult = await _studentFeeService.ProcessPaymentAsync(request.PaymentRequest);
            return paymentResult is null
                ? ResponseDto.Fail("Payment could not be processed. Check student ID and selected months.")
                : ResponseDto.Success(paymentResult, "Payment processed successfully.");
        }
    }
}

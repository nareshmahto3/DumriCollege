namespace Student.Api.DTOs
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = string.Empty;
        public object? Data { get; set; }

        public static ResponseDto Fail(string message = "FAILED") => new ResponseDto { IsSuccess = false, Message = message };
        public static ResponseDto Success(object? data, string message = "SUCCESS") => new ResponseDto { Data = data, IsSuccess = true, Message = message };
    }
}

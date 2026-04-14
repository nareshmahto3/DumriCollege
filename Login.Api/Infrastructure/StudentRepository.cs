
namespace Login.Api.Infrastructure
{
    using Login.Api.DbConnection;
    using Login.Api.DbEntities;
    using Microsoft.EntityFrameworkCore;

    public class StudentRepository
    {
        private readonly LoginContext _context;

        public StudentRepository(LoginContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByPhoneAsync(string phoneNumber)
        {
            var normalizedPhone = phoneNumber.Trim(); // remove spaces
            return await _context.Students
                .FirstOrDefaultAsync(s => s.PhoneNumber.Trim() == normalizedPhone);
        }

        public async Task UpdateOtpAsync(Student student, int? otp, DateTime? expiry)
        {
            student.Otp = otp;
            student.OtpExpiry = expiry;

            // Only call Update if the entity is detached
            if (_context.Entry(student).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Students.Update(student);
            }

            await _context.SaveChangesAsync();
        }
    }
}
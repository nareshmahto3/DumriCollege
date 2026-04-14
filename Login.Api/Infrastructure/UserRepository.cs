using Login.Api.DbConnection;
using Login.Api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.Infrastructure  // ✅ Add a proper namespace
{
    public class UserRepository
    {
        private readonly LoginContext _context;

        public UserRepository(LoginContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task UpdatePasswordAsync(User user, string newPassword)
        {
            user.Password = newPassword;
            await _context.SaveChangesAsync();
        }

        public async Task SaveOtpAsync(string phone, int otp)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            if (user == null) return;

            user.Otp = otp;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(10);

            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetValidOtpAsync(string phone, int otp)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.PhoneNumber == phone &&
                u.Otp == otp
            );
        }
    }
}
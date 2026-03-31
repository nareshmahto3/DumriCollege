using Microsoft.EntityFrameworkCore;
using User.Api.DbConnection;

//using User.Api.DbConnection;
using User.Api.DTOs;
using User.Api.Utility;

namespace User.Api.Services
{
    public class UserServiceRepo 
    {
        //private readonly DumriCommerceCollegeContext _context;

        //public UserServiceRepo(DumriCommerceCollegeContext context)
        //{
        //    _context = context;
        //}

        //public async Task<IEnumerable<User.Api.DbEntities.User>> GetAllUsersAsync() =>
        //    await _context.Users.ToListAsync();

        //public async Task<User.Api.DbEntities.User> GetUserByIdAsync(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //        throw new Exception($"User with ID {id} not found.");

        //    return user;
        //}
        public async Task AddUserAsync(UserCreateDto user)
        {
            var entity = new User.Api.DbEntities.User
            {
                //CreatedDate = DateTime.Now,
                //DateOfBirth = user.DateOfBirth,
                //Email = user.Email,
                //IsActive = true,
                //Mobile = user.Mobile,
                //Name = user.Name,
                //PasswordHash = PasswordHelper.HashPassword(user.Password)
            };


            //_context.Users.Add(entity);
            //await _context.SaveChangesAsync();
        }

        //public async Task UpdateUserAsync(UserCreateDto userDto)
        //{
        //    var entity = await _context.Users.FindAsync(userDto.Email);

        //    if (entity == null)
        //    {
        //        throw new Exception($"User with email {userDto.Email} not found.");
        //    }

        //    // Update fields
        //    // entity.DateOfBirth = userDto.DateOfBirth;
        //    // entity.Email = userDto.Email;
        //    //entity.Mobile = userDto.Mobile;
        //    //entity.Name = userDto.Name;
        //    //  entity.IsActive = true;
        //    // entity.UpdatedDate = DateTime.UtcNow;
        //    _context.Users.Update(entity);
        //    await _context.SaveChangesAsync();
        //}


        //public async Task DeleteUserAsync(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}

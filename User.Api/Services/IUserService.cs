using User.Api.DTOs;

namespace User.Api.Services
{
    public interface IUserService
    {
        //Task<IEnumerable<User.Api.DbEntities.User>> GetAllUsersAsync();
        //Task<User.Api.DbEntities.User> GetUserByIdAsync(int id);
        Task AddUserAsync(UserCreateDto user);
        Task UpdateUserAsync(UserCreateDto user);
        Task DeleteUserAsync(int id);
    }
}

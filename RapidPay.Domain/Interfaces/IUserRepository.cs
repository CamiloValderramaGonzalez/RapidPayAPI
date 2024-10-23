using RapidPay.Domain.Entities;

namespace RapidPay.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}

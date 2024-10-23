using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Interfaces;
using RapidPay.Infrastructure.Data;

namespace RapidPay.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RapidPayContext _context;

        public UserRepository(RapidPayContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}

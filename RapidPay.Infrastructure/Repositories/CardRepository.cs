using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Interfaces;
using RapidPay.Infrastructure.Data;

namespace RapidPay.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly RapidPayContext _context;

        public CardRepository(RapidPayContext context)
        {
            _context = context;
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            return await _context.Cards.FindAsync(id);
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task AddAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }
    }
}

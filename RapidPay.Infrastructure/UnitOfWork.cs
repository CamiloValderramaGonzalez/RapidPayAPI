using RapidPay.Domain.Interfaces;
using RapidPay.Infrastructure.Data;
using RapidPay.Infrastructure.Repositories;

namespace RapidPay.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RapidPayContext _context;
        public ICardRepository Cards { get; }

        public UnitOfWork(RapidPayContext context)
        {
            _context = context;
            Cards = new CardRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using RapidPay.Domain.Entities;

namespace RapidPay.Domain.Interfaces
{
    public interface ICardRepository
    {
        Task<Card> GetByIdAsync(int id);
        Task<IEnumerable<Card>> GetAllAsync();
        Task AddAsync(Card card);
        Task UpdateAsync(Card card);
    }
}

using RapidPay.Domain.Entities;

namespace RapidPay.Application.Interfaces
{
    public interface ICardService
    {
        Task<Card> CreateCardAsync();
        Task<Card> GetCardByIdAsync(int id);
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<bool> PayAsync(int id, decimal amount);
        Task<bool> AddBalanceAsync(int id, decimal amount);
    }
}

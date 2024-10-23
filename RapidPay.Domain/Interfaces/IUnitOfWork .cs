namespace RapidPay.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICardRepository Cards { get; }
        Task<int> CompleteAsync();
    }
}

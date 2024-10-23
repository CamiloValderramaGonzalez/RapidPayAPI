using RapidPay.Application.Interfaces;
using RapidPay.Domain.Entities;
using RapidPay.Domain.Interfaces;

namespace RapidPay.Application.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeeService _feeService;

        public CardService(IUnitOfWork unitOfWork, IFeeService feeService)
        {
            _unitOfWork = unitOfWork;
            _feeService = feeService;
        }

        public async Task<Card> CreateCardAsync()
        {
            var newCard = new Card
            {
                CardNumber = GenerateCardNumber(),
                Balance = 0.0m
            };

            await _unitOfWork.Cards.AddAsync(newCard);
            await _unitOfWork.CompleteAsync();

            return newCard;
        }

        public async Task<Card> GetCardByIdAsync(int id)
        {
            return await _unitOfWork.Cards.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _unitOfWork.Cards.GetAllAsync();
        }

        public async Task<bool> PayAsync(int id, decimal amount)
        {
            var card = await _unitOfWork.Cards.GetByIdAsync(id);
            if (card == null)
            {
                return false;
            }

            var feeRate = _feeService.GetCurrentFeeRate();
            var feeAmount = amount * feeRate;
            var totalAmount = amount + feeAmount;

            if (card.Balance < totalAmount)
            {
                return false;
            }

            card.Balance -= totalAmount;
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> AddBalanceAsync(int id, decimal amount)
        {
            var card = await _unitOfWork.Cards.GetByIdAsync(id);
            if (card == null)
            {
                return false;
            }

            card.Balance = amount;
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private string GenerateCardNumber()
        {
            var random = new Random();
            return new string(Enumerable.Repeat("0123456789", 15)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

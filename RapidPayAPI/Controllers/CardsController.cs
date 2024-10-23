using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Application.Interfaces;
using RapidPay.Domain.Entities;

namespace RapidPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Card>> CreateCard()
        {
            var newCard = await Task.Run(() => _cardService.CreateCardAsync());
            return CreatedAtAction(nameof(GetCardBalance), new { id = newCard.Id }, newCard);
        }

        [HttpGet("GetBalance/{id}")]
        public async Task<ActionResult<decimal>> GetCardBalance(int id)
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null)
            {
                return NotFound("Card not found.");
            }

            return card.Balance;
        }

        [HttpPut("Pay/{id}")]
        public async Task<IActionResult> Pay(int id, [FromBody] PaymentRequest paymentRequest)
        {
            await _semaphore.WaitAsync(); 
            try
            {
                var result = await _cardService.PayAsync(id, paymentRequest.Amount);
                if (!result)
                {
                    return BadRequest("Invalid payment request or insufficient balance.");
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return NoContent();
        }

        [HttpPut("AddBalance/{id}")]
        public async Task<IActionResult> AddBalance(int id, [FromBody] PaymentRequest paymentRequest)
        {
            await _semaphore.WaitAsync();  
            try
            {
                var result = await _cardService.AddBalanceAsync(id, paymentRequest.Amount);
                if (!result)
                {
                    return BadRequest("Invalid balance request.");
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return NoContent();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Card>>> GetAllCards()
        {
            var cards = await Task.Run(() => _cardService.GetAllCardsAsync());
            return Ok(cards);
        }
    }

    public class PaymentRequest
    {
        public decimal Amount { get; set; }
    }
}

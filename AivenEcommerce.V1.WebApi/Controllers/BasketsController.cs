using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace AivenEcommerce.V1.WebApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperationResultEnumerable<Basket>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAll()
        {
            var baskets = await _basketRepository.GetAllAsync();
            return Ok(baskets);
        }

        [HttpGet("{id:length(24)}", Name = "GetBasket")]
        [ProducesResponseType(typeof(OperationResult<Basket>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<ActionResult<Basket>> Get(string id)
        {
            var book = await _basketRepository.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("[action]/user/{userId}")]
        [ProducesResponseType(typeof(OperationResult<Basket>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public  ActionResult<Basket> GetOpenByUser(Guid userId)
        {
            var basket = _basketRepository.GetBasketOpenByUser(new User
            {
                Id = userId
            });

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<Basket>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(Basket basket)
        {
            var basketCreated = await _basketRepository.CreateAsync(basket);

            return CreatedAtRoute("GetBasket", new { id = basketCreated.Id.ToString() }, basketCreated);
        }
    }
}

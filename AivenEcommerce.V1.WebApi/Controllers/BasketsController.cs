using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace AivenEcommerce.V1.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Basket>), 200)]
        public IActionResult Get()
        {
            var baskets = _basketRepository.GetAll();
            return Ok(baskets);
        }

        [HttpGet("{id:length(24)}", Name = "GetBasket")]
        [ProducesResponseType(typeof(Basket), 200)]
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
        [ProducesResponseType(typeof(Basket), 200)]
        public  ActionResult<Basket> GetOpenByUser(Guid userId)
        {
            var basket = _basketRepository.GetBasketOpenByUser(new Domain.Entities.User
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
        [ProducesResponseType(typeof(Basket), 200)]
        public async Task<IActionResult> Create(Basket basket)
        {
            var basketCreated = await _basketRepository.CreateAsync(basket);

            return CreatedAtRoute("GetBasket", new { id = basketCreated.Id.ToString() }, basketCreated);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TestPryanikyV2.Data.Entities;
using TestPryanikyV2.Data.Interfaces;

namespace TestPryanikyV2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Получение списка заказов
        /// </summary>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Получение заказа по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            return Ok(order);
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <responce code="201">Success</responce>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            await _unitOfWork.Orders.AddAsync(order);
            return CreatedAtRoute("GetOrder", new { Id = order.Id }, null);
        }

        /// <summary>
        /// Обновление заказа
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <responce code="204">Success</responce>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] Order order)
        {
            await _unitOfWork.Orders.UpdateAsync(order);
            return NoContent();
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <responce code="204">Success</responce>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Orders.DeleteAsync(id);
            return NoContent();
        }
    }
}

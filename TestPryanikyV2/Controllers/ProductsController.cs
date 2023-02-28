using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestPryanikyV2.Data.Entities;
using TestPryanikyV2.Data.Interfaces;

namespace TestPryanikyV2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Получение списка продуктов
        /// </summary>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Получение продукта по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Создание продукта
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <responce code="201">Success</responce>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            return CreatedAtRoute("GetProduct", new { Id = product.Id }, null);
        }

        /// <summary>
        /// Обновление продукта
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <responce code="204">Success</responce>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            await _unitOfWork.Products.UpdateAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <responce code="204">Success</responce>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Products.DeleteAsync(id);
            return NoContent();
        }
    }
}

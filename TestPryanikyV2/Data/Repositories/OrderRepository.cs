using Microsoft.EntityFrameworkCore;
using TestPryanikyV2.Data.Context;
using TestPryanikyV2.Data.Entities;
using TestPryanikyV2.Data.Interfaces;

namespace TestPryanikyV2.Data.Repositories
{
    public class OrderRepository : IDataModel<Order>
    {
        private readonly PryanikyDbContext _context;

        public OrderRepository(PryanikyDbContext context) => _context = context;

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _context.Orders.Include(order => order.Products).ToListAsync();

        public async Task<Order> GetByIdAsync(int id) =>
            await _context.Orders.Include(order => order.Products).FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            var orderFromDb = await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(x => x.Id == order.Id);
            orderFromDb.Products = new List<Product>();

            if (orderFromDb is null) return;

            orderFromDb.Name = order.Name;
            orderFromDb.Products = order.Products;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order is null) return;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}

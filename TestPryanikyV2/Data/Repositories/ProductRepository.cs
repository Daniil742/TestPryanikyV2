using Microsoft.EntityFrameworkCore;
using TestPryanikyV2.Data.Context;
using TestPryanikyV2.Data.Entities;
using TestPryanikyV2.Data.Interfaces;

namespace TestPryanikyV2.Data.Repositories
{
    public class ProductRepository : IDataModel<Product>
    {
        private readonly PryanikyDbContext _context;

        public ProductRepository(PryanikyDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.Include(product => product.Orders).ToListAsync();

        public async Task<Product> GetByIdAsync(int id) =>
            await _context.Products.Include(product => product.Orders).FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var productFromDb = await _context.Products.Include(p => p.Orders).FirstOrDefaultAsync(x => x.Id == product.Id);

            if (productFromDb is null) return;

            productFromDb.Name = product.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null) return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TestPryanikyV2.Data.Context;
using TestPryanikyV2.Data.Entities;
using TestPryanikyV2.Data.Interfaces;

namespace TestPryanikyV2.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly PryanikyDbContext _context;
        private OrderRepository? _orderRepository;
        private ProductRepository? _productRepository;

        public UnitOfWork(PryanikyDbContext context) => _context = context;

        public IDataModel<Order> Orders
        {
            get
            {
                if (_orderRepository is null)
                    _orderRepository = new OrderRepository(_context);

                return _orderRepository;
            }
        }

        public IDataModel<Product> Products
        {
            get
            {
                if (_productRepository is null)
                    _productRepository = new ProductRepository(_context);

                return _productRepository;
            }
        }

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

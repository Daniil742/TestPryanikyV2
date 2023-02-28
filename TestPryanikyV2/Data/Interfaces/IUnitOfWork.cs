using TestPryanikyV2.Data.Entities;

namespace TestPryanikyV2.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDataModel<Order> Orders { get; }
        IDataModel<Product> Products { get; }
        Task SaveAsync();
    }
}

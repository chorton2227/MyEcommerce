namespace MyEcommerce.Core.Domain
{
    using System.Threading.Tasks;
    
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<bool> SaveChangesAsync();
    }
}
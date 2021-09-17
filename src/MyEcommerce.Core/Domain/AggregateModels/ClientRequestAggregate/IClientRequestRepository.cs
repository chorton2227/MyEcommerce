namespace MyEcommerce.Core.Domain.AggregateModels.ClientRequestAggregate
{
    using System.Threading.Tasks;

    public interface IClientRequestRepository : IRepository<ClientRequest>
    {
        Task<bool> ExistsAsync(ClientRequestId cientRequestId);

        Task<ClientRequest> CreateAsync(ClientRequestId cientRequestId);
    }
}
using CosmosExampleAPI.Models;

namespace CosmosExampleAPI.Interface
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Elements>> GetMultipleAsync(string query);

        Task<Elements> GetAsync(string id);

        Task AddAsync(Elements element);

        Task UpdateAsync(string id,Elements element);

        Task DeleteAsync(string id);

    }
}

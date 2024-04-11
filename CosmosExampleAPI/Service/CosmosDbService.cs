using CosmosExampleAPI.Interface;
using CosmosExampleAPI.Models;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;

namespace CosmosExampleAPI.Service
{
    public class CosmosDbService : ICosmosDbService
    {

        private readonly Container _container;

        public CosmosDbService(CosmosClient cosmosDbClient, string databaseName, string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }


        public async Task AddAsync(Elements element)
        {
           await _container.CreateItemAsync(element, new PartitionKey(element.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Elements>(id, new PartitionKey(id));
        }

        public async Task<Elements> GetAsync(string id)
        {
            try
            {
               var response = await _container.ReadItemAsync<Elements>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null!;
            }
        }

        public async Task<IEnumerable<Elements>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Elements>(new QueryDefinition(queryString));
            var results = new List<Elements>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, Elements element)
        {
            await _container.UpsertItemAsync(element, new PartitionKey(id));
        }
    }
}

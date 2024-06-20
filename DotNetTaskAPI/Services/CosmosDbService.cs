using DotNetTaskAPI.Models.Domain;
using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;

namespace DotNetTaskAPI.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient cosmosClient, string databaseId, string containerId)
        {
            _container = cosmosClient.GetContainer(databaseId, containerId);
        }

        public async Task AddQuestionAsync(Question question)
        {
            await _container.CreateItemAsync(question, new PartitionKey(question.QuestionId));
        }

        public async Task UpdateQuestionAsync(string id, Question question)
        {
            await _container.UpsertItemAsync(question, new PartitionKey(id));
        }

        public async Task<Question> GetQuestionAsync(string id)
        {
            try
            {
                ItemResponse<Question> response = await _container.ReadItemAsync<Question>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var query = _container.GetItemQueryIterator<Question>(new QueryDefinition("SELECT * FROM Questions"));
            List<Question> results = new List<Question>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}

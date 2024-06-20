using DotNetTaskAPI.Models.Domain;

namespace DotNetTaskAPI.Services
{
    public interface ICosmosDbService
    {
        Task AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(string id, Question question);
        Task<Question> GetQuestionAsync(string id);
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}

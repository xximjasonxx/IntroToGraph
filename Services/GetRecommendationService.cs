using GraphDemo.Models;

namespace GraphDemo.Services
{
    public class GetRecommendationService : IGetRecommendationService
    {
        private IQuerySource _querySource;

        public Task<Recommendation> GetRecommendationAsync(Guid userId)
        {
            //throw new NotImplementedException();
            return Task.FromResult(new Recommendation());
        }
    }

    public interface IGetRecommendationService
    {
        Task<Recommendation> GetRecommendationAsync(Guid userId);
    }
}
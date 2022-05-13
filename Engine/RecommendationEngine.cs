using System;
using GraphDemo.Entities;
using GraphDemo.Models;
using GraphDemo.Services;
using GraphDemo.Extensions;

namespace GraphDemo.Engine
{
	public class RecommendationEngine : IRecommendationEngine
	{
		private readonly IQuerySource _querySource;

		public RecommendationEngine(IQuerySource querySource)
		{
			_querySource = querySource;
		}

        public async Task<Recommendation> GetRecommendationForUserAsync(User user)
        {
			var likedArtists = await _querySource.GetEdgedVertices<User, Artist, LikeArtist>(user);
			var genresOrderedByLikedFrequency = Constants.AvailableGenres
				.Select(genre => new
				{
					genre,
					matchedGenreLikes = likedArtists.Count(artist => artist.Genre == genre),
					totalLikes = likedArtists.Count
				})
				.Where(x => x.matchedGenreLikes > 0)
				.Select(x => new { Genre = x.genre, PercentOfLikes = (decimal)x.matchedGenreLikes / x.totalLikes })
				.OrderByDescending(x => x.PercentOfLikes)
				.Select(x => x.Genre)
				.ToList();

			foreach (var genre in genresOrderedByLikedFrequency)
            {
				// get the user's friends
				var friends = await _querySource.GetEdgedVertices<User, User, Friend>(user);
				foreach (var friend in friends)
                {
					var friendLikedArtists = await _querySource.GetEdgedVertices<User, Artist, LikeArtist>(friend);
					var potentialNewArtist = friendLikedArtists.Filter(likedArtists).FirstOrDefault();
                }
            }
		}
    }

	public interface IRecommendationEngine
    {
		Task<Recommendation> GetRecommendationForUserAsync(User user);

	}
}


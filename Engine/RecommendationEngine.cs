using System;
using GraphDemo.Entities;
using GraphDemo.Models;
using GraphDemo.Services;
using GraphDemo.Extensions;
using GraphDemo.Entities.Support;

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
			var likedArtists = await _querySource.GetSingleEdgedVertices<User, Artist, LikeArtist>(user);
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

			// search the likes of our connected friends
			var friends = await _querySource.GetDoubleEdgedVertices<User, User, Friend>(user);
			var recommendation = await SearchFriends(genresOrderedByLikedFrequency, friends, likedArtists);
			if (recommendation != null)
				return recommendation;


			// search all available artists using Genre as a guidepost
			var allArtists = await _querySource.GetVertices<Artist>();
			var availableArtists = allArtists.Filter(likedArtists, new ArtistEqualityComparer()).ToList();
			recommendation = SearchAll(genresOrderedByLikedFrequency, availableArtists);
			if (recommendation != null)
				return recommendation;


			// just pick a random one from the set available
			recommendation = PickRandom(availableArtists);
			if (recommendation != null)
				return recommendation;

			// nothing is available - return null
			return null;
		}

		async Task<Recommendation> SearchFriends(IList<string> genrePreferenceOrder, IList<User> friends, IList<Artist> userLikedArtists)
        {
			foreach (var genre in genrePreferenceOrder)
			{
				// look through our friends likes for this genre
				foreach (var friend in friends)
				{
					var friendLikedArtists = await _querySource.GetSingleEdgedVertices<User, Artist, LikeArtist>(friend);
					var potentialNewArtist = friendLikedArtists.Filter(userLikedArtists, new ArtistEqualityComparer())
						.FirstOrDefault(x => x.Genre == genre);

					if (potentialNewArtist != null)
						return new Recommendation { ArtistId = potentialNewArtist.Id, ArtistName = potentialNewArtist.Name };
				}
			}

			return null;
		}

		Recommendation SearchAll(IList<string> genrePreferenceOrder, IList<Artist> artists)
        {
			foreach (var genre in genrePreferenceOrder)
			{
				var potentialNewArtist = artists.FirstOrDefault(x => x.Genre == genre);
				if (potentialNewArtist != null)
					return new Recommendation { ArtistId = potentialNewArtist.Id, ArtistName = potentialNewArtist.Name };
			}

			return null;
		}

		Recommendation PickRandom(IList<Artist> availableArtists)
        {
			var random = new Random(DateTime.Now.Second);
			var recommendedArtist = availableArtists.ElementAtOrDefault(random.Next(0, availableArtists.Count - 1));
			if (recommendedArtist != null)
				return new Recommendation { ArtistId = recommendedArtist.Id, ArtistName = recommendedArtist.Name };

			return null;
		}
    }

	public interface IRecommendationEngine
    {
		Task<Recommendation> GetRecommendationForUserAsync(User user);

	}
}


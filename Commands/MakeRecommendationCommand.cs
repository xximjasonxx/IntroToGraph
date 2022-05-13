﻿using System;
using GraphDemo.Entities;
using GraphDemo.Property;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
	public class MakeRecommendationCommand : ICommand
	{
		private readonly IQuerySource _querySource;
		private readonly IGetRecommendationService _getRecommendationService;

		public MakeRecommendationCommand(IQuerySource querySource, IGetRecommendationService getRecommendationService)
		{
			_querySource = querySource;
			_getRecommendationService = getRecommendationService;
		}

        public async Task ExecuteAsync()
        {
            // select the user to make a recommendation for
			var users = await _querySource.GetVertices<User>();
			var propertyWriter = new PropertyWriter<User>();

			Guid selectedUserId = Guid.Empty;
            propertyWriter.SetProperty<User>(
                promptText: "Pick user: ",
                titleText: "Select a user to get recommendations for",
                availableOptions: users,
                objectText: user => user.Name,
                setter: user => selectedUserId = user.Id);

			// get the recommendations for the user
			var recommendation = await _getRecommendationService.GetRecommendationAsync(selectedUserId);

			// ask for acceptance
			Console.Write("Accept Recommendation (y/n): ");
			var acceptYes = Console.ReadLine()?.ToLower() == "y";

			// if yes, create the link
			if (acceptYes)
			{
				var likeArtist = new LikeArtist
				{
					UserId = selectedUserId,
					ArtistId = recommendation.ArtistId
				};

				await _querySource.AddEdgeAsync(likeArtist);
			}
        }
    }
}

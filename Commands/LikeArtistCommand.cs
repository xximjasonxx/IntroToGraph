using System;
using GraphDemo.Entities;
using GraphDemo.Property;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
	public class LikeArtistCommand : ICommand
	{
        private readonly IQuerySource _querySource;

		public LikeArtistCommand(IQuerySource querySource)
		{
            _querySource = querySource;
		}

        public async Task ExecuteAsync()
        {
            // query the current list of artist
            var artists = await _querySource.GetVertices<Artist>();
            var users = await _querySource.GetVertices<User>();
            
            // set properties
            var likedArtist = new LikeArtist() { Id = Guid.NewGuid() };
            var propertyWriter = new PropertyWriter<LikeArtist>();

            propertyWriter.SetProperty<User>(
                promptText: "Pick user: ",
                titleText: "Select a user to like the artist",
                availableOptions: users,
                objectText: user => user.Name,
                setter: user => likedArtist.UserId = user.Id);
            Console.WriteLine();

            propertyWriter.SetProperty<Artist>(
                promptText: "Pick artist: ",
                titleText: "Select the artist to like",
                availableOptions: artists,
                objectText: artist => artist.Name,
                setter: artist => likedArtist.ArtistId = artist.Id);

            // add the edge
            await _querySource.AddEdge(likedArtist);
            Console.WriteLine("Edge Added");
        }
    }
}


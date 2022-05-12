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
            
            // set properties
            var likedArtist = new LikeArtist() { Id = Guid.NewGuid() };
            var propertyWriter = new PropertyWriter<LikeArtist>();

            propertyWriter.SetProperty<Artist>(
                promptText: "Pick artist: ",
                titleText: "Select the artist you like",
                availableOptions: artists,
                objectText: artist => artist.Name,
                setter: artist => likedArtist.ArtistId = artist.Id);
        }
    }
}


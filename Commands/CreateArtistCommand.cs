using GraphDemo.Entities;
using GraphDemo.Property;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class CreateArtistCommand : ICommand
    {
        private readonly IQuerySource _querySource;

        public CreateArtistCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            var artist = new Artist() { Id = Guid.NewGuid() };
            var propertyWriter = new PropertyWriter<Artist>();

            // ask for properties
            propertyWriter.SetProperty("Enter Artist Name: ", value => artist.Name = value);
            propertyWriter.SetProperty("Select Genre: ", Constants.AvailableGenres, value => artist.Genre = value);

            // save the song
            var createdSong = await _querySource.AddVertex(artist);
            Console.WriteLine("Artist Added");
        }
    }
}
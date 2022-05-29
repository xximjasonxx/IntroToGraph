using GraphDemo.Entities;
using GraphDemo.Extensions;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class GetArtistInfoCommand : ICommand
    {
        private readonly IQuerySource _querySource;

        public GetArtistInfoCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Select an artist to get info for");
            var artists = await _querySource.GetVertices<Artist>();
            for (int i = 0; i < artists.Count; i++)
            {
                Console.WriteLine($" {i + 1}) {artists[i].Name}");
            }

            Console.Write("Enter selection: ");
            var selectionValue = Console.ReadLine()?.AsInt();

            var selectedArtist = artists.ElementAtOrDefault(selectionValue.HasValue ? selectionValue.Value - 1 : int.MinValue);
            if (selectedArtist == null)
            {
                Console.WriteLine("Invalid selection");
                return;
            }

            Console.WriteLine($"Name: {selectedArtist.Name}");
            Console.WriteLine($"Genre: {selectedArtist.Genre}");

            var likes = await _querySource.CountVertexEdges<Artist, LikeArtist>(selectedArtist);
        }
    }
}
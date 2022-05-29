using GraphDemo.Entities;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class CountDataCommand : ICommand
    {
        private readonly IQuerySource _querySource;

        public CountDataCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            var userCount = await _querySource.CountVertices<User>();
            var artistCount = await _querySource.CountVertices<Artist>();
            var friendshipsCount = await _querySource.CountEdges<Friend>();
            var likeArtistCount = await _querySource.CountEdges<LikeArtist>();

            Console.WriteLine($"Users: {userCount}");
            Console.WriteLine($"Artists: {artistCount}");
            Console.WriteLine($"Friendships: {friendshipsCount}");
            Console.WriteLine($"Liked Artists: {likeArtistCount}");
        }
    }
}
using GraphDemo.Entities;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class SeedDataCommand : ICommand
    {
        private readonly IQuerySource _querySource;

        public SeedDataCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            // clear the database
            await _querySource.DropAllVertices();
            Console.WriteLine("Clear Complete");

            // add a set of artists
            var artist1 = await _querySource.AddVertex(new Artist()
            {
                Genre = "Alternative",
                Name = "Goo Goo Dolls"
            });
            Console.WriteLine("Artist Create Complete");

            var artist2 = await _querySource.AddVertex(new Artist()
            {
                Genre = "Rock",
                Name = "Metallica"
            });
            Console.WriteLine("Artist Create Complete");

            await _querySource.AddVertex(new Artist()
            {
                Genre = "Pop",
                Name = "Taylor Swift"
            });
            Console.WriteLine("Artist Create Complete");

            await _querySource.AddVertex(new Artist()
            {
                Genre = "Pop",
                Name = "NSYNC"
            });
            Console.WriteLine("Artist Create Complete");

            await _querySource.AddVertex(new Artist()
            {
                Genre = "Rock",
                Name = "Skillet"
            });
            Console.WriteLine("Artist Create Complete");

            await _querySource.AddVertex(new Artist()
            {
                Genre = "Country",
                Name = "George Strait"
            });
            Console.WriteLine("Artist Create Complete");

            await _querySource.AddVertex(new Artist()
            {
                Genre = "Alternative",
                Name = "Third Eye Blind"
            });
            Console.WriteLine("Artist Create Complete");

            await _querySource.AddVertex(new Artist()
            {
                Genre = "Rock",
                Name = "Pop Evil"
            });
            Console.WriteLine("Artist Create Complete");

            var artist3 = await _querySource.AddVertex(new Artist()
            {
                Genre = "K-Pop",
                Name = "BTS"
            });
            Console.WriteLine("Artist Create Complete");

            // add Jason User
            var user1 = await _querySource.AddVertex(new User()
            {
                FirstName = "Jason",
                LastName = "Farrell"
            });
            Console.WriteLine("User Create Complete");

            var user2 = await _querySource.AddVertex(new User()
            {
                FirstName = "Claire",
                LastName = "Farrell"
            });
            Console.WriteLine("User Create Complete");

            // add friendship
            await _querySource.AddEdge(new Friend
            {
                Id = Guid.NewGuid(),
                SourceFriendId = user1.Id,
                TargetFriendId = user2.Id
            });

            await _querySource.AddEdge(new Friend
            {
                Id = Guid.NewGuid(),
                SourceFriendId = user2.Id,
                TargetFriendId = user1.Id
            });
            Console.WriteLine("Friend Create Complete");

            // add some relationships
            await _querySource.AddEdge(new LikeArtist
            {
                Id = Guid.NewGuid(),
                ArtistId = artist1.Id,
                UserId = user1.Id
            });
            Console.WriteLine("Artist Like Create Complete");

            await _querySource.AddEdge(new LikeArtist
            {
                Id = Guid.NewGuid(),
                ArtistId = artist2.Id,
                UserId = user1.Id
            });
            Console.WriteLine("Artist Like Create Complete");

            await _querySource.AddEdge(new LikeArtist
            {
                Id = Guid.NewGuid(),
                ArtistId = artist3.Id,
                UserId = user2.Id
            });
            Console.WriteLine("Artist Like Create Complete");

            Console.WriteLine("database seeded");
        }
    }
}
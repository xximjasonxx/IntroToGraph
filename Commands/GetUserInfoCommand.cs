using GraphDemo.Entities;
using GraphDemo.Extensions;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class GetUserInfoCommand : ICommand
    {
        private readonly IQuerySource _querySource;

        public GetUserInfoCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Select a user to get info for");
            var users = await _querySource.GetVertices<User>();
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($" {i + 1}) {users[i].Name}");
            }

            Console.Write("Enter selection: ");
            var selectionValue = Console.ReadLine()?.AsInt();

            var selectedUser = users.ElementAtOrDefault(selectionValue.HasValue ? selectionValue.Value - 1 : int.MinValue);
            if (selectedUser == null)
            {
                Console.WriteLine("Invalid selection");
                return;
            }

            Console.WriteLine($"Name: {selectedUser.Name}");
            Console.WriteLine("Friends");
            var friends = await _querySource.GetSingleEdgedVertices<User, User, Friend>(selectedUser);
            foreach (var friend in friends)
            {
                Console.WriteLine($" {friend.Name}");
            }
            Console.WriteLine();

            Console.WriteLine("Favorite Artists");
            var likedArtists = await _querySource.GetSingleEdgedVertices<User, Artist, LikeArtist>(selectedUser);
            foreach (var likedArtist in likedArtists)
            {
                Console.WriteLine($" {likedArtist.Name}");
            }

            Console.Write("Press [Enter] to continue...");
            Console.Read();
        }
    }
}
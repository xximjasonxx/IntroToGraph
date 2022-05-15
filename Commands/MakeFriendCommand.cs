using GraphDemo.Entities;
using GraphDemo.Property;
using GraphDemo.Services;

namespace GraphDemo.Commands
{
    public class MakeFriendCommand : ICommand
    {
        private readonly IQuerySource _querySource;

        public MakeFriendCommand(IQuerySource querySource)
        {
            _querySource = querySource;
        }

        public async Task ExecuteAsync()
        {
            // get all users
            var users = await _querySource.GetVertices<User>();
            var friendLink = new Friend() { Id = Guid.NewGuid() };

            // set the initiating friend
            var propertyWriter = new PropertyWriter<Friend>();
            propertyWriter.SetProperty<User>(
                promptText: "Pick user: ",
                titleText: "Select a user to initiate the friend relationship",
                availableOptions: users,
                objectText: user => user.Name,
                setter: user => friendLink.SourceFriendId = user.Id);
            Console.WriteLine();

            // set the destination friend
            var availableUsers = users.Where(user => user.Id != friendLink.SourceFriendId).ToList();
            propertyWriter.SetProperty<User>(
                promptText: "Pick user: ",
                titleText: "Select a user to initiate the friend relationship",
                availableOptions: availableUsers,
                objectText: user => user.Name,
                setter: user => friendLink.TargetFriendId = user.Id);

            // create the link
            await _querySource.AddEdge(friendLink);

            Console.WriteLine("Edge Added");
        }
    }
}
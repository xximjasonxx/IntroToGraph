using GraphDemo.Commands;
using GraphDemo.Engine;
using GraphDemo.MenuView;
using GraphDemo.Services;
using Microsoft.Extensions.Hosting;

namespace GraphDemo
{
    public class Application : IHostedService
    {
        public readonly IQuerySource _querySource;
        public readonly IRecommendationEngine _recommendationEngine;

        public Application(IQuerySource querySource, IRecommendationEngine recommendationEngine)
        {
            _querySource = querySource;
            _recommendationEngine = recommendationEngine;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            do
            {
                // build the menu
                var menu = new MenuBuilder()
                    .AddOption(1, "Create artist", () => new CreateArtistCommand(_querySource))
                    .AddOption(2, "Create a user", () => new CreateUserCommand(_querySource))
                    .AddOption(3, "Like an artist", () => new LikeArtistCommand(_querySource))
                    .AddOption(4, "Make friend", () => new MakeFriendCommand(_querySource))
                    .AddOption(5, "Recommend artist", () => new MakeRecommendationCommand(_querySource, _recommendationEngine))
                    .AddExitOption(6, "Exit")
                    .AddPrompText("Please select an Option: ")
                    .Build();

                menu.Show();
                var optionSelectedRaw = Console.ReadLine();
                var selectedOption = menu.SelectOption(optionSelectedRaw ?? string.Empty);
                Console.WriteLine();

                if (selectedOption.IsExit)
                    break;      // exit the program

                await selectedOption.Command.ExecuteAsync();
                Console.WriteLine();

            } while (true);

            await this.StopAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("End");
            return Task.CompletedTask;
        }
    }
}
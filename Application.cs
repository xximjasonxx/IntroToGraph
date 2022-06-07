using GraphDemo.Commands;
using GraphDemo.Engine;
using GraphDemo.MenuView;
using GraphDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GraphDemo
{
    public class Application : IHostedService
    {
        public readonly IQuerySource _querySource;
        public readonly IConfiguration _configuration;
        public readonly IRecommendationEngine _recommendationEngine;

        public Application(IQuerySource querySource, IRecommendationEngine recommendationEngine,
            IConfiguration configuration)
        {
            _querySource = querySource;
            _configuration = configuration;
            _recommendationEngine = recommendationEngine;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            do
            {
                try
                {
                    // build the menu
                    var menu = new MenuBuilder()
                        .AddOption(1, "Create artist", () => new CreateArtistCommand(_querySource))
                        .AddOption(2, "Create a user", () => new CreateUserCommand(_querySource))
                        .AddOption(3, "Like an artist", () => new LikeArtistCommand(_querySource))
                        .AddOption(4, "Make friend", () => new MakeFriendCommand(_querySource))
                        .AddOption(5, "Recommend artist", () => new MakeRecommendationCommand(_querySource, _recommendationEngine))
                        .AddOption(6, "Get User Info", () => new GetUserInfoCommand(_querySource))
                        .AddOption(7, "Get Artist Info", () => new GetArtistInfoCommand(_querySource))
                        .AddOption(8, "Count data", () => new CountDataCommand(_querySource))
                        .AddOption(9, "Run Test Script", () => new TestCommand(_configuration))
                        .AddExitOption(10, "Exit")
                        .AddPrompText("Please select an Option: ")
                        .Build();

                    menu.Show();
                    var optionSelectedRaw = Console.ReadLine();
                    var selectedOption = menu.SelectOption(optionSelectedRaw ?? string.Empty);
                    Console.WriteLine();

                    if (selectedOption.IsExit)
                        break;      // exit the program

                    await selectedOption.Command.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occrured with that request");
                    Console.WriteLine(ex.Message);
                }

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
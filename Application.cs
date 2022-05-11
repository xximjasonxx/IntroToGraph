using GraphDemo.Commands;
using GraphDemo.Menu;
using Microsoft.Extensions.Hosting;

namespace GraphDemo
{
    public class Application : IHostedService
    {
        public readonly CommandResolver _commandResolver;

        public Application(CommandResolver commandResolver)
        {
            _commandResolver = commandResolver;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            ApplicationOption selectedAction;

            do
            {
                // build the menu
                var menu = new MenuBuilder<ApplicationOption>()
                    .AddOption(1, "Create a song", ApplicationOption.CreateSong)
                    .AddOption(2, "Exit", ApplicationOption.ExitProgram)
                    .AddPrompText("Please select an Option: ")
                    .Build();

                menu.Show();
                var optionSelectedRaw = Console.ReadLine();
                selectedAction = menu.SelectOption(optionSelectedRaw ?? string.Empty);
                Console.WriteLine();

                if (selectedAction != ApplicationOption.ExitProgram)
                {
                    var command = _commandResolver.CreateCommand(selectedAction);
                    await command.ExecuteAsync();
                    Console.WriteLine();
                }

            } while (selectedAction != ApplicationOption.ExitProgram);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("End");
        }
    }
}
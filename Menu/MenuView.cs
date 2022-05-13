using GraphDemo.Extensions;

namespace GraphDemo.MenuView
{
    public class MenuView
    {
        private readonly string _promptText;
        private readonly IList<MenuOption> _options;

        public MenuView(string promptText, IList<MenuOption> options)
        {
            _promptText = promptText;
            _options = options;
        }

        public void Show()
        {
            foreach (var option in _options)
            {
                Console.WriteLine($"{option.OptionNumber}) {option.OptionText}");
            }

            Console.Write(_promptText);
        }

        public MenuOption SelectOption(string optionSelectedRaw)
        {
            var optionSelectedParsed = optionSelectedRaw.AsInt();
            if (!optionSelectedParsed.HasValue)
                throw new InvalidOperationException("Option provided was not in numeric form");

            int selectedOptionValue = optionSelectedParsed.Value;
            if (selectedOptionValue < 1 || selectedOptionValue > _options.Count)
                throw new InvalidOperationException("Option provided was not in range");

            var selectedOption = _options.FirstOrDefault(o => o.OptionNumber == selectedOptionValue);
            if (selectedOption == null)
                throw new InvalidOperationException("Option provided was not valid");

            return selectedOption;
        }
    }
}
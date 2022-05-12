
using GraphDemo.Extensions;

namespace GraphDemo.Property
{
    public class PropertyWriter<TType> where TType : class
    {
        public void SetProperty(string promptText, Action<string> setter)
        {
            Console.Write(promptText);
            var value = Console.ReadLine();
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"No value provided for promot '{promptText}'");

            setter.Invoke(value);
        }

        public void SetProperty(string promptText, string[] availableOptions, Action<string> setter)
        {
            for (int i=0; i<availableOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {availableOptions[i]}");
            }
            Console.Write(promptText);

            var selectedOptionNumber = Console.ReadLine()?.AsInt();
            if (selectedOptionNumber.HasValue == false)
                throw new InvalidOperationException("Did not select a valid option");

            var selectedOption = selectedOptionNumber.Value - 1;
            if (selectedOption < 0 || selectedOption >= availableOptions.Length)
                throw new InvalidOperationException("Selected option is out of range of options");

            setter.Invoke(availableOptions[selectedOption]);
        }
    }
}
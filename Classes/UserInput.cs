using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace Flashcards.harris_andy
{
    public class UserInput
    {
        public int GetMenuChoice(int start, int end)
        {
            int menuChoice = AnsiConsole.Prompt(
            new TextPrompt<int>("Menu choice:")
            .Validate((n) =>
            {
                if (start <= n && n <= end)
                    return ValidationResult.Success();

                else
                    return ValidationResult.Error($"[red]Pick a valid option[/]");
            }));
            return menuChoice;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace Flashcards.harris_andy
{
    public class UserInput
    {
        public int GetMenuChoice(int start, int end, string text)
        {
            int menuChoice = AnsiConsole.Prompt(
            new TextPrompt<int>(text)
            .Validate((n) =>
            {
                if (start <= n && n <= end)
                    return ValidationResult.Success();

                else
                    return ValidationResult.Error($"[red]Pick a valid option[/]");
            }));
            return menuChoice;
        }

        public string GetText(string message)
        {
            string flashCardText = AnsiConsole.Prompt(
                new TextPrompt<string>(message)
            );
            return flashCardText;
        }

        public string ChooseNewOrOldStack()
        {
            // Console.Clear();
            string answer = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Add to an [green]existing stack[/] or [yellow]create a new stack[/]?")
                    .PageSize(3)
                    // .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] {
                        "Choose existing", "Create new", "Main Menu"
                    }));
            return answer.ToLower();
        }

        public int GetQuestionPoints()
        {
            string answer = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Was your answer [green]correct[/] or [red]wrong[/]?")
                    .PageSize(3)
                    .AddChoices(new[] {
                        "Correct", "Wrong"
                    }));
            if (answer == "Correct")
                return 1;
            else
                return 0;
        }

        public int VerifyStackID(List<Stack> stackData)
        {
            if (stackData.Count == 0)
            {
                Console.WriteLine("No stacks found!");
                Thread.Sleep(2000);
                // NewFlashCard();
                return 0;
            }
            var maxID = stackData
                .OrderByDescending(s => s.Id)
                .FirstOrDefault()?.Id ?? 0;
            var validIDs = stackData
                .Select(s => s.Id)
                .ToList();
            var verifiedStackID = AnsiConsole.Prompt(
            new TextPrompt<int>($"Enter stack ID to continue:")
            .Validate((n) =>
            {
                if (validIDs.Contains(n))
                    return ValidationResult.Success();

                else
                    return ValidationResult.Error($"[red]Must be a valid ID[/]");
            }));
            return verifiedStackID;
        }

        public void WaitToContinue()
        {
            Console.WriteLine($"Press enter to continue...");
            Console.Read();
        }

        // public string GetStackName(string message)
        // {
        //     UseDB usingDB = new UseDB();
        //     List<Stack> stackData = usingDB.GetAllStackNames();
        //     var names = stackData.Select(n => n.Name);
        // }

        // public int CreateOrSelectStack()
        // {
        //     int choice = AnsiConsole.Prompt(
        //         new TextPrompt<int>("How do you want your stack?\n1. Choose stack from list\n2. Create new stack")
        //         .Validate((n) =>
        //         {
        //             if (n == 1 || n == 2)
        //                 return ValidationResult.Success();
        //             else
        //                 return ValidationResult.Error($"[red]Must be a valid choice[/]");
        //         }));
        //     return choice;
        // }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace Flashcards.harris_andy
{
    public class DisplayData
    {
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine(
                "--------------------------------------------------\n" +
                "\n\t\tMAIN MENU\n\n" +
                "\tWhat would you like to do?\n\n" +
                "\tType 0 to Close Application\n" +
                "\tType 1 to Study Flashcards\n" +
                "\tType 2 to Create a New Flash Card\n" +
                "\tType 3 to Create a New Stack\n" +
                "\tType 4 to Delete a Flash Card\n" +
                "\tType 5 to Delete a Stack\n" +
                "\tType 6 to View Study Session Scores\n" +
                // "\tType 7 to Add 100 Rows of Fake Data\n" +
                // "\tType 8 to Start a Timed Coding Session. Neat!\n" +
                // "\tType 9 to Set a Coding Goal\n" +
                // "\tType 10 to Get Coding Goal Progress\n" +
                "--------------------------------------------------\n");
        }

        public void ShowStackNames(List<Stack> stackData)
        {
            var table = new Table();
            bool isAlternateRow = false;

            table.BorderColor(Color.DarkSlateGray1);
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[cyan1]ID[/]").LeftAligned());
            table.AddColumn(new TableColumn("[blue1]Name[/]").RightAligned());

            foreach (Stack stack in stackData)
            {
                var color = isAlternateRow ? "grey" : "blue";
                table.AddRow(
                    $"[{color}]{stack.Id}[/]",
                    $"[{color}]{stack.Name ?? "N/A"}[/]"
                );
                isAlternateRow = !isAlternateRow;
            }
            Console.Clear();
            AnsiConsole.Write(table);
            // Console.WriteLine("Press any key to continue...");
            // Console.Read();
        }
    }
}
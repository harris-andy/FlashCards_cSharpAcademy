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

        public void ShowStackMessage(List<Stack> stackData, int stackID, string message)
        {
            var stackName = stackData
                    .Where(s => s.Id == stackID)
                    .Select(s => s.Name)
                    .FirstOrDefault();
            Console.WriteLine($"{message} {stackName}.");
            Thread.Sleep(2000);
        }

        public void DisplayCard(string text, int index)
        {
            Console.Clear();
            Panel panel = new Panel(text);
            panel.Header = new PanelHeader($"[blue]Flash Card: {index}[/]");
            panel.HeaderAlignment(Justify.Center);
            // panel.Border = BoxBorder.Ascii;
            // panel.Border = BoxBorder.Square;
            panel.Border = BoxBorder.Double;
            panel.Border = BoxBorder.Rounded;
            // panel.Border = BoxBorder.Heavy;
            // panel.Border = BoxBorder.None;
            panel.Padding = new Padding(10, 5, 10, 5);
            AnsiConsole.Write(panel);
        }

        public void DisplayScore(int score, int questions)
        {
            Console.Clear();
            string text = $"You got {score}/{questions} correct.";
            Panel panel = new Panel(text);
            panel.Header = new PanelHeader($"[yellow]Score[/]");
            panel.HeaderAlignment(Justify.Center);
            // panel.Border = BoxBorder.Ascii;
            // panel.Border = BoxBorder.Square;
            // panel.Border = BoxBorder.Double;
            // panel.Border = BoxBorder.Rounded;
            panel.Border = BoxBorder.Heavy;
            panel.BorderColor(Color.Yellow);
            // panel.Border = BoxBorder.None;
            panel.Padding = new Padding(10, 5, 10, 5);
            AnsiConsole.Write(panel);
        }
    }
}
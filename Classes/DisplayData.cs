using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.harris_andy.Classes;
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
                "\tType 4 to Delete a Stack\n" +
                "\tType 5 to View Study Sessions\n" +
                "\tType 6 to View Study Sessions COUNT by Month\n" +
                "\tType 7 to View Study Sessions GRADES by Month\n" +
                "\tType 8 to Add Fake Data\n" +
                "\tType 9 to Add Fake Study Sessions\n" +
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
            table.AddColumn(new TableColumn("[green1]Name[/]").RightAligned());
            // table.AddColumn(new TableColumn("[blue1]Study Sessions[/]").LeftAligned());

            foreach (Stack stack in stackData)
            {
                var color = isAlternateRow ? "grey" : "blue";
                table.AddRow(
                    $"[{color}]{stack.Id}[/]",
                    $"[{color}]{stack.Name ?? "N/A"}[/]"
                // $"[{color}]{stack.Sessions}[/]"
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

        public void ShowStudySessions(List<StudySessionDTO> records, string name)
        {
            var table = new Table();
            bool isAlternateRow = false;

            table.BorderColor(Color.DarkSlateGray1);
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[cyan1]Date[/]").LeftAligned());
            table.AddColumn(new TableColumn("[green1]Subject[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]Score[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow1]Questions[/]").RightAligned());
            table.AddColumn(new TableColumn("[red1]% Correct[/]").LeftAligned());

            // table.AddColumn(new TableColumn("[cyan1]ID[/]").LeftAligned());
            // table.AddColumn(new TableColumn("[green1]Start Date[/]").RightAligned());
            // table.AddColumn(new TableColumn("[blue1]End Date[/]").RightAligned());
            // table.AddColumn(new TableColumn("[yellow1]Goal Hours[/]").RightAligned());
            // table.AddColumn(new TableColumn("[red]Complete?[/]").LeftAligned());

            foreach (StudySessionDTO record in records)
            {
                string grade = (record.Score / (float)record.Questions).ToString("P1");
                var color = isAlternateRow ? "grey" : "blue";
                table.AddRow(
                    $"[{color}]{record.Date.ToShortDateString()}[/]",
                    $"[{color}]{name}[/]",
                    $"[{color}]{record.Score}[/]",
                    $"[{color}]{record.Questions}[/]",
                    $"[{color}]{grade}[/]"
                );
                isAlternateRow = !isAlternateRow;
            }
            Console.Clear();
            AnsiConsole.Write(table);
            // Console.WriteLine("Press any key to continue...");
            // Console.Read();
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

        public void NothingFound(string item)
        {
            Console.Clear();
            Console.WriteLine($"No {item} found!");
            Thread.Sleep(2000);
        }

        public void ShowStudySessionCounts(List<StudyReportCounts> records, string title)
        {
            var table = new Table();
            bool isAlternateRow = false;

            table.Title(title);
            table.BorderColor(Color.DarkSlateGray1);
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[cyan1]Stack Name[/]").LeftAligned());
            table.AddColumn(new TableColumn("[green1]January[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]February[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow1]March[/]").RightAligned());
            table.AddColumn(new TableColumn("[red1]April[/]").RightAligned());
            table.AddColumn(new TableColumn("[cyan1]May[/]").RightAligned());
            table.AddColumn(new TableColumn("[green1]June[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]July[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow1]August[/]").RightAligned());
            table.AddColumn(new TableColumn("[green1]September[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]October[/]").RightAligned());
            table.AddColumn(new TableColumn("[cyan1]November[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow]December[/]").LeftAligned());

            foreach (StudyReportCounts record in records)
            {
                var color = isAlternateRow ? "grey" : "blue";
                table.AddRow(
                    $"[{color}]{record.StackName}[/]",
                    $"[{color}]{record.January}[/]",
                    $"[{color}]{record.February}[/]",
                    $"[{color}]{record.March}[/]",
                    $"[{color}]{record.April}[/]",
                    $"[{color}]{record.May}[/]",
                    $"[{color}]{record.June}[/]",
                    $"[{color}]{record.July}[/]",
                    $"[{color}]{record.August}[/]",
                    $"[{color}]{record.September}[/]",
                    $"[{color}]{record.October}[/]",
                    $"[{color}]{record.November}[/]",
                    $"[{color}]{record.December}[/]"
                );
                isAlternateRow = !isAlternateRow;
            }
            Console.Clear();
            AnsiConsole.Write(table);
        }

        public void ShowStudySessionGrades(List<StudyReportGrades> records, string title, string caster)
        {
            var table = new Table();
            bool isAlternateRow = false;

            table.Title(title);
            table.BorderColor(Color.DarkSlateGray1);
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[cyan1]Stack Name[/]").LeftAligned());
            table.AddColumn(new TableColumn("[green1]January[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]February[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow1]March[/]").RightAligned());
            table.AddColumn(new TableColumn("[red1]April[/]").RightAligned());
            table.AddColumn(new TableColumn("[cyan1]May[/]").RightAligned());
            table.AddColumn(new TableColumn("[green1]June[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]July[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow1]August[/]").RightAligned());
            table.AddColumn(new TableColumn("[green1]September[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]October[/]").RightAligned());
            table.AddColumn(new TableColumn("[cyan1]November[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow]December[/]").LeftAligned());

            foreach (StudyReportGrades record in records)
            {
                var color = isAlternateRow ? "grey" : "blue";
                table.AddRow(
                    $"[{color}]{record.StackName}[/]",
                    $"[{color}]{record.January.ToString("P1")}[/]",
                    $"[{color}]{record.February.ToString("P1")}[/]",
                    $"[{color}]{record.March.ToString("P1")}[/]",
                    $"[{color}]{record.April.ToString("P1")}[/]",
                    $"[{color}]{record.May.ToString("P1")}[/]",
                    $"[{color}]{record.June.ToString("P1")}[/]",
                    $"[{color}]{record.July.ToString("P1")}[/]",
                    $"[{color}]{record.August.ToString("P1")}[/]",
                    $"[{color}]{record.September.ToString("P1")}[/]",
                    $"[{color}]{record.October.ToString("P1")}[/]",
                    $"[{color}]{record.November.ToString("P1")}[/]",
                    $"[{color}]{record.December.ToString("P1")}[/]"
                );
                isAlternateRow = !isAlternateRow;
            }
            Console.Clear();
            AnsiConsole.Write(table);
        }

        public void ShowStudySessionTEST(List<StudyReport> records, string title)
        {
            var table = new Table();
            bool isAlternateRow = false;

            table.Title(title);
            table.BorderColor(Color.DarkSlateGray1);
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[cyan1]Stack Name[/]").LeftAligned());
            table.AddColumn(new TableColumn("[green1]January[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]February[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow1]March[/]").RightAligned());
            table.AddColumn(new TableColumn("[red1]April[/]").RightAligned());
            table.AddColumn(new TableColumn("[cyan1]May[/]").RightAligned());
            table.AddColumn(new TableColumn("[green1]June[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]July[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow1]August[/]").RightAligned());
            table.AddColumn(new TableColumn("[green1]September[/]").RightAligned());
            table.AddColumn(new TableColumn("[blue1]October[/]").RightAligned());
            table.AddColumn(new TableColumn("[cyan1]November[/]").RightAligned());
            table.AddColumn(new TableColumn("[yellow]December[/]").LeftAligned());

            foreach (StudyReport record in records)
            {
                var color = isAlternateRow ? "grey" : "blue";
                table.AddRow(
                    $"[{color}]{record.StackName}[/]",
                    $"[{color}]{record.January}[/]",
                    $"[{color}]{record.February}[/]",
                    $"[{color}]{record.March}[/]",
                    $"[{color}]{record.April}[/]",
                    $"[{color}]{record.May}[/]",
                    $"[{color}]{record.June}[/]",
                    $"[{color}]{record.July}[/]",
                    $"[{color}]{record.August}[/]",
                    $"[{color}]{record.September}[/]",
                    $"[{color}]{record.October}[/]",
                    $"[{color}]{record.November}[/]",
                    $"[{color}]{record.December}[/]"
                );
                isAlternateRow = !isAlternateRow;
            }
            Console.Clear();
            AnsiConsole.Write(table);
        }
    }
}

// $"[{color}]{((double)record.January).ToString("P1")}[/]",
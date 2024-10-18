using Flashcards.harris_andy;
using Microsoft.Data.SqlClient;
using Dapper;

internal class Program
{
    private static void Main(string[] args)
    {

        UserInput userInput = new UserInput();
        DisplayData displayData = new DisplayData();
        UseDB useDB = new UseDB();
        FlashCardController controller = new FlashCardController(displayData, userInput, useDB);

        controller.ShowMainMenu();

    }
}

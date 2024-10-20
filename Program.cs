/*
Requirements

    The study and stack tables should be linked. If a stack is deleted, it's study sessions should be deleted.

    The project should contain a call to the study table so the users can see all their study sessions. This table receives insert calls upon each study session, but there shouldn't be update and delete calls to it.

BUGS
    - CreateNewStack always prints CW line
    - 
*/

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

        controller.InitializeDatabase();
        controller.ShowMainMenu();

    }
}

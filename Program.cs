/*
Requirements

    You should use DTOs to show the flashcards to the user without the Id of the stack it belongs to.

    When showing a stack to the user, the flashcard Ids should always start with 1 without gaps between them. If you have 10 cards and number 5 is deleted, the table should show Ids from 1 to 9.

    After creating the flashcards functionalities, create a "Study Session" area, where the users will study the stacks. All study sessions should be stored, with date and score.

    The study and stack tables should be linked. If a stack is deleted, it's study sessions should be deleted.

    The project should contain a call to the study table so the users can see all their study sessions. This table receives insert calls upon each study session, but there shouldn't be update and delete calls to it.

BUGS
    1. CreateNewStack always prints CW line
    2. FlashCard IDs are fucked up (1002, 1003)
    3. 
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

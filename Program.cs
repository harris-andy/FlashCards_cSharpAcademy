using Flashcards.harris_andy;

internal class Program
{
    private static void Main(string[] args)
    {
        UserInput userInput = new UserInput();
        DisplayData displayData = new DisplayData();
        DataManager dataManager = new DataManager();
        FlashCardController controller = new FlashCardController(displayData, userInput, dataManager);

        controller.InitializeDatabase();
        controller.ShowMainMenu();
    }
}

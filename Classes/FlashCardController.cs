using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy
{
    public class FlashCardController
    {
        private readonly DisplayData _displayData;
        private readonly UserInput _userInput;
        private readonly UseDB _useDB;

        public FlashCardController(DisplayData displayData, UserInput userInput, UseDB useDB)
        {
            _displayData = displayData;
            _userInput = userInput;
            _useDB = useDB;
        }

        public void InitializeDatabase()
        {
            _useDB.InitializeDatabase();
        }

        public void ShowMainMenu()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                _displayData.MainMenu();
                int inputNumber = _userInput.GetMenuChoice(0, 6, "Menu choice:");
                switch (inputNumber)
                {
                    case 0:
                        Console.WriteLine("\nBye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case 1:
                        // study session
                        break;
                    case 2:
                        NewFlashCard();
                        break;
                    case 3:
                        CreateNewStack();
                        break;
                    case 4:
                        // delete a flash card
                        break;
                    case 5:
                        DeleteStack();
                        break;
                    case 6:
                        // view study session scores
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nInvalid Command. Give me number!");
                        break;
                }
            }
        }

        public void NewFlashCard()
        {
            // Console.Clear();
            // string chooseStackText = _userInput.ChooseNewOrOldStack();
            int stackID = ChooseStack();
            bool closeApp = false;
            // if (chooseStackText == "create new")
            //     stackID = CreateNewStack();
            // if (chooseStackText == "choose existing")
            // {
            //     List<Stack> stackData = _useDB.GetAllStackNames();
            //     if (stackData.Count == 0)
            //     {
            //         Console.WriteLine("No stacks found!");
            //         Thread.Sleep(2000);
            //         NewFlashCard();
            //     }
            //     _displayData.ShowStackNames(stackData);
            //     stackID = _userInput.VerifyStackID(stackData);
            // }
            // if (chooseStackText == "main menu")
            //     ShowMainMenu();

            while (closeApp == false)
            {
                Console.Clear();
                string messageFront = $"Enter text for the flashcard FRONT:";
                string messageBack = $"Enter text for the flashcard BACK:";
                string front = _userInput.GetText(messageFront);
                string back = _userInput.GetText(messageBack);
                FlashCard flashCard = new FlashCard(front, back, stackID);
                _useDB.AddFlashCard(flashCard, stackID);

                Console.WriteLine("Press 0 to return to Main Menu or Enter to add more flash cards.");
                ConsoleKeyInfo button = Console.ReadKey(true);
                if (button.Key == ConsoleKey.NumPad0 || button.Key == ConsoleKey.D0)
                {
                    ShowMainMenu();
                    break;
                }
            }
        }

        public int ChooseStack()
        {
            Console.Clear();
            string chooseStackText = _userInput.ChooseNewOrOldStack();
            int stackID = -1;
            if (chooseStackText == "create new")
                stackID = CreateNewStack();
            if (chooseStackText == "choose existing")
            {
                List<Stack> stackData = _useDB.GetAllStackNames();
                if (stackData.Count == 0)
                {
                    Console.WriteLine("No stacks found!");
                    Thread.Sleep(2000);
                    NewFlashCard();
                }
                _displayData.ShowStackNames(stackData);
                stackID = _userInput.VerifyStackID(stackData);
            }
            if (chooseStackText == "main menu")
                ShowMainMenu();
            return stackID;
        }

        public int CreateNewStack()
        {
            string? stackName = null;
            List<Stack> stackData = _useDB.GetAllStackNames();
            var names = stackData.Select(n => n.Name);

            // FIX THIS - SHOULDN'T ALWAYS SHOW THAT CW
            while (stackName == null || names.Contains(stackName))
            {
                string message = $"Enter a name for this new [yellow]flash card stack[/] (no repeats):";
                stackName = _userInput.GetText(message);
                Console.WriteLine("Like I said, no repeats...");
            }
            int stackID = _useDB.CreateStackID(stackName);
            return stackID;
        }

        public void DeleteStack()
        {
            List<Stack> stackData = _useDB.GetAllStackNames();
            _displayData.ShowStackNames(stackData);
            int stackID = _userInput.VerifyStackID(stackData);
            string message = $"Deleted stack";
            _displayData.ShowStackMessage(stackData, stackID, message);
            _useDB.DeleteStack(stackID);
        }
    }
}
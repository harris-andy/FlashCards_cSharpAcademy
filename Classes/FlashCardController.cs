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
            Console.Clear();
            string chooseStackText = _userInput.ChooseNewOrOldStack();
            int stackID = 0;
            bool closeApp = false;
            if (chooseStackText == "create new")
                stackID = CreateNewStack();
            if (chooseStackText == "choose existing")
                stackID = GetStackID();
            if (chooseStackText == "main menu")
                ShowMainMenu();

            while (closeApp == false)
            {
                Console.Clear();
                string messageFront = $"Enter text for the flashcard FRONT:";
                string messageBack = $"Enter text for the flashcard BACK:";
                string front = _userInput.GetText(messageFront);
                string back = _userInput.GetText(messageBack);
                FlashCard flashCard = new FlashCard(front, back, stackID);
                // stackID = AddToStack();
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

        public int GetStackID()
        {
            List<Stack> stackData = _useDB.GetAllStackNames();
            if (stackData.Count == 0)
            {
                Console.WriteLine("No stacks found!");
                Thread.Sleep(2000);
                NewFlashCard();
                return 0;
            }
            _displayData.ShowStackNames(stackData);
            return _userInput.VerifyStackID(stackData);
        }

        // public int AddToStack()
        // {
        //     List<Stack> stackData = _useDB.GetAllStackNames();

        //     if (stackData.Count == 0)
        //         return CreateNewStack();

        //     string chooseStackText = $"\nHow do you want your stack?\n1. Choose an existing stack\n2. Create new stack";
        //     int stackChoice = _userInput.GetMenuChoice(1, 2, chooseStackText);

        //     if (stackChoice == 1)
        //     {
        //         _displayData.ShowStackNames(stackData);
        //         string message = $"Flash card added to ";
        //         int stackID = _userInput.GetMenuChoice(1, stackData.Count, "Choose a stack ID from above to add your flash card to:");
        //         _displayData.ShowStackMessage(stackData, stackID, message);
        //         // var stackName = stackData
        //         //     .Where(s => s.Id == stackID)
        //         //     .Select(s => s.Name)
        //         //     .FirstOrDefault();
        //         // Console.WriteLine($"Flash card added to {stackName}.");
        //         // Thread.Sleep(2000);
        //         return stackID;
        //     }
        //     return CreateNewStack();
        // }

        public int CreateNewStack()
        {
            string? stackName = null;
            List<Stack> stackData = _useDB.GetAllStackNames();
            var names = stackData.Select(n => n.Name);

            // FIX THIS - SHOULDN'T ALWAYS SHOW THAT CW
            while (stackName == null || names.Contains(stackName))
            {
                string message = "Enter a name for this new flash card stack (no repeats):";
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
            int stackID = GetStackID();
            string message = $"Deleted stack";
            _displayData.ShowStackMessage(stackData, stackID, message);
            _useDB.DeleteStack(stackID);
        }
    }
}
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
                        // delete a stack
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
            string messageFront = $"Enter text for the flashcard FRONT:";
            string messageBack = $"Enter text for the flashcard BACK:";
            string front = _userInput.GetText(messageFront);
            string back = _userInput.GetText(messageBack);
            FlashCard flashCard = new FlashCard(front, back);
            int stackID = GetStackID();
            // int flashCardID = _useDB.AddFlashCard(flashCard, stackID);
            _useDB.AddFlashCard(flashCard, stackID);
            // return flashCardID;
        }

        public int GetStackID()
        {
            List<Stack> stackData = _useDB.GetAllStackNames();

            if (stackData.Count == 0)
                return CreateNewStack();

            string chooseStackText = "How do you want your stack?\n1. Choose an existing stack\n2. Create new stack\n\n";
            int stackChoice = _userInput.GetMenuChoice(1, 2, chooseStackText);

            if (stackChoice == 1)
            {
                _displayData.ShowStackNames(stackData);
                return _userInput.GetMenuChoice(1, stackData.Count, "Choose a stack ID from above to add your flash card to:");
            }
            return CreateNewStack();
        }

        public int CreateNewStack()
        {
            string message = "Enter a name for this new flash card stack:";
            string stackName = _userInput.GetText(message);
            int stackID = _useDB.GetOrCreateStackID(stackName);
            return stackID;
        }
    }
}
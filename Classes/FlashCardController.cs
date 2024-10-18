using System;
using System.Collections.Generic;
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
                        AddFlashCard();
                        break;
                    case 3:
                        // add a new stack
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

        public void AddFlashCard()
        {
            string messageFront = $"Enter text for the flashcard FRONT:";
            string messageBack = $"Enter text for the flashcard BACK:";
            string front = _userInput.GetText(messageFront);
            string back = _userInput.GetText(messageBack);
            FlashCard flashCard = new FlashCard(front, back);
            _useDB.AddFlashCard(flashCard);
        }

        public int GetStackChoice()
        {
            List<Stack> stackData = _useDB.GetAllStackNames();
            _displayData.ShowStackNames(stackData);
            string chooseStackText = "How do you want your stack?\n1. Choose stack from list\n2. Create new stack";
            int stackChoice = _userInput.GetMenuChoice(1, 2, chooseStackText);
            int stackID = 0;

            if (stackChoice == 1)
            {
                // create new stack
                string message = "Enter a name for this flash card stack:";
                string stackName = _userInput.GetText(message);
                stackID = _useDB.CreateStack(stackName);
            }
            if (stackChoice == 2)
            {
                // choose stack number
                stackID = _userInput.GetMenuChoice(1, stackData.Count, "Choose a stack ID from above to add your flash card to:");
            }
            return stackID;
        }
    }
}
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
                int inputNumber = _userInput.GetMenuChoice(0, 6);
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
            string front = _userInput.GetFlashCardText("front");
            string back = _userInput.GetFlashCardText("back");
            FlashCard flashCard = new FlashCard(front, back);
            _useDB.AddFlashCard(flashCard);
        }

        public void GetStackName()
        {
            List<Stack> stackData = _useDB.GetAllStackNames();
            _displayData.ShowStackNames(stackData);

        }
    }
}
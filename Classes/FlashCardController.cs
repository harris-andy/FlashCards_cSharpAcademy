using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Flashcards.harris_andy.Classes;

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
                int inputNumber = _userInput.GetMenuChoice(0, 8, "Menu choice:");
                switch (inputNumber)
                {
                    case 0:
                        Console.WriteLine("\nBye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case 1:
                        StudySession();
                        break;
                    case 2:
                        NewFlashCard();
                        break;
                    case 3:
                        CreateNewStack();
                        break;
                    case 4:
                        DeleteStack();
                        break;
                    case 5:
                        ViewStudySessions();
                        break;
                    case 6:
                        StudySessionCounts();
                        break;
                    case 7:
                        AddFakeData();
                        break;
                    case 8:
                        AddFakeStudySessions();
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
            string chooseStackText = _userInput.ChooseNewOrOldStack();
            int stackID = ChooseStack(chooseStackText);
            if (stackID == 0)
                NewFlashCard();
            bool closeApp = false;

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

        public int ChooseStack(string option)
        {
            Console.Clear();
            int stackID = -1;
            if (option == "create new")
                stackID = CreateNewStack();
            if (option == "choose existing")
            {
                List<Stack> stackData = _useDB.GetAllStackNames();
                if (stackData.Count == 0)
                {
                    _displayData.NothingFound("stacks");
                    return 0;
                }
                _displayData.ShowStackNames(stackData);
                stackID = _userInput.VerifyStackID(stackData);
            }
            if (option == "main menu")
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
            int stackID = _useDB.AddStack(stackName);
            return stackID;
        }

        public void DeleteStack()
        {
            List<Stack> stackData = _useDB.GetAllStackNames();
            _displayData.ShowStackNames(stackData);
            int stackID = _userInput.VerifyStackID(stackData);
            string message = $"Deleted stack";
            if (_userInput.ConfirmDelete())
            {
                _displayData.ShowStackMessage(stackData, stackID, message);
                _useDB.DeleteStack(stackID);
            }
        }

        public void StudySession()
        {
            int stackID = ChooseStack("choose existing");
            List<FlashCardDTO> flashCards = _useDB.GetFlashCardDTO(stackID);
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            int score = 0;
            int questions = flashCards.Count();

            for (int index = 0; index < flashCards.Count; index++)
            {
                _displayData.DisplayCard(flashCards[index].Front, index + 1);
                _userInput.WaitToContinue();
                _displayData.DisplayCard(flashCards[index].Back, index + 1);
                score += _userInput.GetQuestionPoints();
            }
            _displayData.DisplayScore(score, questions);
            _userInput.WaitToContinue();
            StudySessionRecord record = new StudySessionRecord(date, score, questions, stackID);
            _useDB.AddStudySession(record);
        }

        public void ViewStudySessions()
        {
            int stackID = ChooseStack("choose existing");
            string stackName = _useDB.GetStackName(stackID);
            List<StudySessionDTO> records = _useDB.GetStudySessionRecords(stackID);
            if (records.Count == 0)
            {
                _displayData.NothingFound("study sessions");
                ViewStudySessions();
            }
            else
            {
                _displayData.ShowStudySessions(records, stackName);
                _userInput.WaitToContinue();
            }
        }

        public void AddFakeData()
        {
            string flashCardsPath = "./SQL_Queries/AddFakeFlashCards.sql";
            _useDB.AddFakeData(flashCardsPath);
        }

        public void AddFakeStudySessions()
        {
            string sessionsPath = "./SQL_Queries/AddFakeStudySessions.sql";
            _useDB.AddFakeData(sessionsPath);
        }

        public void StudySessionCounts()
        {
            List<int> years = _useDB.GetYears();
            List<string> choices = new List<string>();
            foreach (int y in years)
            {
                choices.Add(y.ToString());
            }
            int year = _userInput.ChooseYear(choices);
            string title = $"Study Sessions per Month for: {year}";
            List<StudySessionReport> studySessionCounts = _useDB.GetStudySessionCounts(year);
            _displayData.ShowStudySessionReport(studySessionCounts, title);
            _userInput.WaitToContinue();
        }
    }
}